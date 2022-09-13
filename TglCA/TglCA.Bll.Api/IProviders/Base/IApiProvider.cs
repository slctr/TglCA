using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;

namespace TglCA.Bll.Api.IProviders.Base
{
    public interface IApiProvider
    {
        Task<IEnumerable<BllCurrency>> GetCurrenciesAsync();

        Task<BllCurrency> GetCurrencyAsync(string symbol);

        Task<IEnumerable<ChartPoint<long, decimal>>> GetHistoricalDataAsync(string symbol);

        string GetProviderName();
    }
}
