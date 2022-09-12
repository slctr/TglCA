using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Bll.Api.IProviders.Base
{
    public interface IApiProvider
    {
        Task<IEnumerable<BllCurrency>> GetCurrenciesAsync();

        Task<BllCurrency> GetCurrencyAsync(string symbol);

        Task<IEnumerable<ChartData>> GetHistoricalDataAsync(string symbol);

        string GetProviderName();
    }
}
