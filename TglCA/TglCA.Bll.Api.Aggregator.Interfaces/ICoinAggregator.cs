using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;

namespace TglCA.Bll.Api.Aggregator.Interfaces
{
    public interface ICoinAggregator
    {
        Task<IEnumerable<BllCurrency>> GetAggregatedCurrencies();

        Task<Dictionary<string, BllCurrency>> GetAggregatedCurrency(string symbol);

        Task<Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>> GetAggregatedChart(string symbol);
    }
}
