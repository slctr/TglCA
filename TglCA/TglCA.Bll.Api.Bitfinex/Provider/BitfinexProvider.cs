using Bitfinex.Net.Clients;
using Bitfinex.Net.Enums;
using TglCA.Bll.Api.Providers;
using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Bll.Api.Bitfinex.Provider
{
    public class BitfinexProvider : ApiProvider
    {
        private readonly string QuoteAsset = "USD";

        public readonly BitfinexClient bitfinexClient;

        public BitfinexProvider()
        {
            bitfinexClient = new BitfinexClient();
        }

        public override async Task<IEnumerable<BllCurrency>> GetCurrenciesAsync()
        {
            var task1 = bitfinexClient.SpotApi.ExchangeData.GetAssetsAsync();
            var task2 = bitfinexClient.SpotApi.ExchangeData.GetTickersAsync();

            await Task.WhenAll(task1, task2);

            var currenciesNames = task1.Result;
            var currenciesStats = task2.Result;

            var filteredCurrenciesStats = currenciesStats.Data
                .Where(x => x.Symbol.EndsWith(QuoteAsset))
                .DistinctBy(x => x.Symbol).ToList();

            filteredCurrenciesStats.ForEach(x =>
            {
                var index = x.Symbol.LastIndexOf(QuoteAsset);
                if (index >= 2)
                {
                    x.Symbol = x.Symbol.Substring(1, index - 1);
                }
            });

            var result = from x in currenciesNames.Data
                         join y in filteredCurrenciesStats
                         on x.Name equals y.Symbol
                         select new BllCurrency()
                         {
                             AssetName = x.FullName,
                             Symbol = x.Name,
                             Price = y.LastPrice,
                             Volume24hUsd = y.Volume,
                             PercentChange24h = y.DailyChangePercentage
                         };

            return result;
        }

        public override async Task<BllCurrency> GetCurrencyAsync(string symbol)
        {
            var task1 = bitfinexClient.SpotApi.ExchangeData.GetAssetsAsync();
            var task2 = bitfinexClient.SpotApi.ExchangeData.GetTickerAsync("t" + symbol + QuoteAsset);

            await Task.WhenAll(task1, task2);

            var currencyName = task1.Result.Data.FirstOrDefault(x => x.Name.Equals(symbol));
            var currencyStats = task2.Result;

            return new BllCurrency()
            {
                AssetName = currencyName.FullName,
                Symbol = currencyName.Name,
                Price = currencyStats.Data.LastPrice,
                Volume24hUsd = currencyStats.Data.Volume,
                PercentChange24h = currencyStats.Data.DailyChangePercentage,
            };
        }

        public override async Task<IEnumerable<ChartData>> GetHistoricalDataAsync(string symbol)
        {
            var chart = await bitfinexClient.SpotApi.ExchangeData.GetKlinesAsync("t" + symbol + QuoteAsset,
                KlineInterval.OneHour,
                null,
                250,
                DateTime.Now.AddDays(-7));

            var result = chart.Data.Select(x => new ChartData()
            {
                Price = x.ClosePrice,
                Time = x.OpenTime
            });

            return result;
        }

        public override string GetProviderName()
        {
            return bitfinexClient.SpotApi.CommonSpotClient.ExchangeName;
        }
    }
}
