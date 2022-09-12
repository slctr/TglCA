using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using TglCA.Bll.Api.IProviders.Base;
using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Bll.Api.Providers
{
    public abstract class ApiProvider : IApiProvider
    {
        //public IRestClient client;

        //public ApiProvider(IRestClient restClient)
        //{
        //    client = restClient;
        //}

        //get coins
        public abstract Task<IEnumerable<BllCurrency>> GetCurrenciesAsync();

        public abstract Task<BllCurrency> GetCurrencyAsync(string symbol);

        public abstract Task<IEnumerable<ChartData>> GetHistoricalDataAsync(string symbol);

        public abstract string GetProviderName();
    }
}
