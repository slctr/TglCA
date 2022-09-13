using TglCA.Bll.Api.Aggregator.Interfaces;
using TglCA.Bll.Api.Providers;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Api.Bitfinex.Provider;
using TglCA.Bll.Api.FTX.Provider;
using TglCA.Bll.Api.Kucoin.Provider;
using TglCA.Bll.Interfaces.Entities.Chart;

namespace TglCA.Bll.Api.Aggregator
{
    public class CoinAggregator : ICoinAggregator
    {
        private readonly List<ApiProvider> providers;

        public CoinAggregator()
        {
            providers = new List<ApiProvider>()
            {
                new BitfinexProvider(),
                new KucoinProvider(),
                new FTXProvider()
            };
        }

        public async Task<IEnumerable<BllCurrency>> GetAggregatedCurrencies()
        {
            var tasks = new List<Task<IEnumerable<BllCurrency>>>();
            
            foreach (var p in providers)
            {
                tasks.Add(p.GetCurrenciesAsync());
            }

            var lists = await Task.WhenAll(tasks);

            var currencies = lists.SelectMany(l => l).GroupBy(x => x.Symbol);

            var result = new List<BllCurrency>();

            foreach (var currency in currencies)
            {
                result.Add(new BllCurrency()
                {
                    Symbol = currency.Key,
                    AssetName = currency.First().AssetName,
                    Price = currency.Average(x => x.Price),
                    PercentChange24h = currency.Average(x => x.PercentChange24h),
                    Volume24hUsd = currency.Average(x => x.Volume24hUsd),
                });
            }
            return result;
        }

        public async Task<Dictionary<string, BllCurrency>> GetAggregatedCurrency(string symbol)
        {
            var tasks = new List<Task<BllCurrency>>();

            foreach (var p in providers)
            {
                tasks.Add(p.GetCurrencyAsync(symbol));
            }

            var list = await Task.WhenAll(tasks);

            var result = new Dictionary<string, BllCurrency>();

            for (int i = 0; i < list.Length; i++)
            {
                result.Add(providers[i].GetProviderName(), list[i]);
            }
            return result;
        }

        public async Task<BllCurrency> GetBllCurrency(string symbol)
        {
            var tasks = new List<Task<BllCurrency>>();

            foreach (var p in providers)
            {
                tasks.Add(p.GetCurrencyAsync(symbol));
            }

            var list = await Task.WhenAll(tasks);

            return new BllCurrency()
            {
                Symbol = list.First().Symbol,
                AssetName = list.First().AssetName,
                Price = list.Average(x => x.Price),
                PercentChange24h = list.Average(x => x.PercentChange24h),
                Volume24hUsd = list.Average(x => x.Volume24hUsd)
            };
        }

        public async Task<Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>> GetAggregatedChart(string symbol)
        {
            var tasks = new List<Task<IEnumerable<ChartPoint<long, decimal>>>>();

            foreach (var p in providers)
            {
                tasks.Add(p.GetHistoricalDataAsync(symbol));
            }

            var lists = await Task.WhenAll(tasks);

            var result = new Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>();

            for (int i = 0; i < lists.Length; i++)
            {
                result.Add(providers[i].GetProviderName(), lists[i]);
            }
            return result;
        }

        public async Task<Dictionary<string, ChartPoint<long, decimal>>> GetCurrentChartPoint(string symbol)
        {
            var tasks = new List<Task<ChartPoint<long, decimal>>>();

            foreach (var p in providers)
            {
                tasks.Add(p.GetChartPointAsync(symbol));
            }

            var list = await Task.WhenAll(tasks);

            var result = new Dictionary<string, ChartPoint<long, decimal>>();

            for (int i = 0; i < list.Length; i++)
            {
                result.Add(providers[i].GetProviderName(), list[i]);
            }
            return result;
        }
    }
}
