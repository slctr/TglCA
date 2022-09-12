using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Bll.Api.Aggregator.Interfaces
{
    public interface ICoinAggregator
    {
        Task<IEnumerable<BllCurrency>> GetAggregatedCurrencies();

        Task<Dictionary<string, BllCurrency>> GetAggregatedCurrency(string symbol);

        Task<Dictionary<string, IEnumerable<ChartData>>> GetAggregatedChart(string symbol);
    }
}
