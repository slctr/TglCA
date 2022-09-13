using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using TglCA.Bll.Api.IProviders.Base;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;

namespace TglCA.Bll.Api.Providers
{
    public abstract class ApiProvider : IApiProvider
    {
        public abstract Task<ChartPoint<long, decimal>> GetChartPointAsync(string symbol);

        public abstract Task<IEnumerable<BllCurrency>> GetCurrenciesAsync();

        public abstract Task<BllCurrency> GetCurrencyAsync(string symbol);

        public abstract Task<IEnumerable<ChartPoint<long, decimal>>> GetHistoricalDataAsync(string symbol);

        public abstract string GetProviderName();
    }
}
