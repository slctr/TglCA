using TglCA.Bll.Api.Aggregator.Interfaces;
using TglCA.Bll.Api.Providers;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Api.Bitfinex.Provider;
using TglCA.Bll.Api.FTX.Provider;
using TglCA.Bll.Api.Kucoin.Provider;


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

            var lists = await Task.WhenAll(tasks);

            var result = new Dictionary<string, BllCurrency>();

            for (int i = 0; i < lists.Length; i++)
            {
                result.Add(providers[i].GetProviderName(), lists[i]);
            }
            return result;
        }

        public async Task<Dictionary<string, IEnumerable<ChartData>>> GetAggregatedChart(string symbol)
        {
            var tasks = new List<Task<IEnumerable<ChartData>>>();

            foreach (var p in providers)
            {
                tasks.Add(p.GetHistoricalDataAsync(symbol));
            }

            var lists = await Task.WhenAll(tasks);

            var result = new Dictionary<string, IEnumerable<ChartData>>();

            for (int i = 0; i < lists.Length; i++)
            {
                result.Add(providers[i].GetProviderName(), lists[i]);
            }
            return result;
        }


    }
}
