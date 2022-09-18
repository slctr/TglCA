using FTX.Net.Clients;
using FTX.Net.Enums;
using TglCA.Bll.Api.Providers;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Utils;

namespace TglCA.Bll.Api.FTX.Provider
{
    public class FTXProvider : ApiProvider
    {
        private readonly string QuoteAsset = "USD";

        public readonly FTXClient ftxClient;

        public FTXProvider()
        {
            ftxClient = new FTXClient();
        }

        public override async Task<IEnumerable<BllCurrency>> GetCurrenciesAsync()
        {
            var task1 = ftxClient.TradeApi.ExchangeData.GetAssetsAsync();
            var task2 = ftxClient.TradeApi.ExchangeData.GetSymbolsAsync();

            await Task.WhenAll(task1, task2);

            var currenciesNames = task1.Result;
            var currenciesStats = task2.Result;

            var filteredCurrenciesStats = currenciesStats.Data
                .Where(x => x.Name.EndsWith(QuoteAsset))
                .DistinctBy(x => x.BaseAsset).ToList();

            // Removing USD or USDT symbols.
            filteredCurrenciesStats.ForEach(x =>
            {
                var symbolSpan = new Span<char>(x.Name.ToCharArray());
                x.Name = symbolSpan.Slice(0, symbolSpan.IndexOf('/')).ToString();
            });

            var result = from x in currenciesNames.Data
                         join y in filteredCurrenciesStats
                         on x.Name equals y.Name
                         select new BllCurrency()
                         {
                             AssetName = x.FullName,
                             Symbol = x.Name,
                             Price = y.CurrentPrice.HasValue ? y.CurrentPrice.Value : decimal.MinValue,
                             Volume24hUsd = y.QuoteVolume24H,
                             PercentChange24h = y.Change24Hour,
                         };
            Console.WriteLine(currenciesNames.Data.Count());
            return result;
        }

        public override async Task<BllCurrency> GetCurrencyAsync(string symbol)
        {
            var task1 = ftxClient.TradeApi.ExchangeData.GetAssetsAsync();
            var task2 = ftxClient.TradeApi.ExchangeData.GetSymbolAsync(symbol + "/" + QuoteAsset);

            await Task.WhenAll(task1, task2);

            var currencyName = task1.Result.Data.FirstOrDefault(x => x.Name.Equals(symbol));
            var currencyStats = task2.Result;

            if (currencyName == null ||
                currencyStats.Data == null)
            {
                return null;
            }

            return new BllCurrency()
            {
                AssetName = currencyName.FullName,
                Symbol = currencyName.Name,
                Price = (decimal)currencyStats.Data.CurrentPrice,
                PercentChange24h = currencyStats.Data.Change24Hour,
                Volume24hUsd = currencyStats.Data.QuoteVolume24H
            };
        }

        public override async Task<IEnumerable<ChartPoint<long, decimal>>> GetHistoricalDataAsync(string symbol)
        {
            var chart = await ftxClient.TradeApi.ExchangeData.GetKlinesAsync(symbol + "/" + QuoteAsset,
                KlineInterval.OneMinute,
                DateTime.Now.AddHours(-5), DateTime.Now);

            if (chart.Data == null)
            {
                return null;
            }

            var result = chart.Data
                .Select(x => new ChartPoint<long, decimal>(
                    DateTimeHelper.ToUnixTimestamp(x.OpenTime),
                    x.ClosePrice));

            result = result.OrderByDescending(x => x.X);

            return result;
        }

        public override string GetProviderName()
        {
            return ftxClient.TradeApi.CommonSpotClient.ExchangeName;
        }

        public override async Task<ChartPoint<long, decimal>> GetChartPointAsync(string symbol)
        {
            var currencyStats = await ftxClient.TradeApi.ExchangeData.GetSymbolAsync(symbol + "/" + QuoteAsset);

            if (currencyStats.Data.CurrentPrice == null)
            {
                return null;
            }

            return new ChartPoint<long, decimal>(DateTimeHelper.UnixTimestampNow(), (decimal)currencyStats.Data.CurrentPrice);
        }
    }
}
