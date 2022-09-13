using Kucoin.Net.Clients;
using Kucoin.Net.Enums;
using TglCA.Bll.Api.Providers;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Utils;

namespace TglCA.Bll.Api.Kucoin.Provider
{
    public class KucoinProvider : ApiProvider
    {
        public readonly KucoinClient kucoinClient;

        private readonly string QuoteAsset = "-USDT";

        public KucoinProvider()
        {
            kucoinClient = new KucoinClient();
        }

        public override async Task<IEnumerable<BllCurrency>> GetCurrenciesAsync()
        {
            var task1 = kucoinClient.SpotApi.ExchangeData.GetAssetsAsync();
            var task2 = kucoinClient.SpotApi.ExchangeData.GetTickersAsync();

            await Task.WhenAll(task1, task2);

            var currenciesNames = task1.Result;
            var currenciesStats = task2.Result;

            var filteredCurrenciesStats = currenciesStats.Data.Data
                .Where(x => x.Symbol.EndsWith(QuoteAsset))
                .DistinctBy(x => x.Symbol).ToList();

            // Removing USD or USDT symbols.
            filteredCurrenciesStats.ForEach(x =>
            {
                var symbolSpan = new Span<char>(x.Symbol.ToCharArray());
                x.Symbol = symbolSpan.Slice(0, symbolSpan.IndexOf('-')).ToString();
            });

            var result = from x in currenciesNames.Data
                         join y in filteredCurrenciesStats
                         on x.Asset equals y.Symbol
                         select new BllCurrency()
                         {
                             AssetName = x.FullName,
                             Symbol = x.Asset,
                             Price = (decimal)y.LastPrice,
                             Volume24hUsd = (decimal)y.QuoteVolume,
                             PercentChange24h = (decimal)y.ChangePercentage
                         };

            return result;
        }

        public override async Task<BllCurrency> GetCurrencyAsync(string symbol)
        {
            var task1 = kucoinClient.SpotApi.ExchangeData.GetAssetAsync(symbol);
            var task2 = kucoinClient.SpotApi.ExchangeData.Get24HourStatsAsync(symbol + QuoteAsset);

            await Task.WhenAll(task1, task2);

            var currencyName = task1.Result;
            var currencyStats = task2.Result;

            return new BllCurrency()
            {
                AssetName = currencyName.Data.FullName,
                Symbol = currencyName.Data.Asset,
                Price = (decimal)currencyStats.Data.LastPrice,
                PercentChange24h = (decimal)currencyStats.Data.ChangePercentage,
                Volume24hUsd = (decimal)currencyStats.Data.QuoteVolume
            };
        }

        public override async Task<IEnumerable<ChartPoint<long, decimal>>> GetHistoricalDataAsync(string symbol)
        {
            var chart = await kucoinClient.SpotApi.ExchangeData.GetKlinesAsync(symbol + QuoteAsset,
                KlineInterval.OneMinute,
                DateTime.Now.AddDays(-1));

            var result = chart.Data
                .Select(x => new ChartPoint<long, decimal>(
                    DateTimeHelper.ToUnixTimestamp(x.OpenTime),
                    x.ClosePrice));

            return result;
        }

        public override string GetProviderName()
        {
            return kucoinClient.SpotApi.CommonSpotClient.ExchangeName;
        }
    }
}
