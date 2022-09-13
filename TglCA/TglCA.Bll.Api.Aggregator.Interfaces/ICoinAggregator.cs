using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;

namespace TglCA.Bll.Api.Aggregator.Interfaces
{
    public interface ICoinAggregator
    {
        Task<IEnumerable<BllCurrency>> GetAggregatedCurrencies();

        /// <summary>
        /// Aggregates currencies from different API's into dictionary, where key is marketplace name.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>Dictionary of currencies</returns>
        Task<Dictionary<string, BllCurrency>> GetAggregatedCurrency(string symbol);

        /// <summary>
        /// Aggregates values from different API's into single average currency.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>BllCurrency object of average currency.</returns>
        Task<BllCurrency> GetBllCurrency(string symbol);

        Task<Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>> GetAggregatedChart(string symbol);

        Task<Dictionary<string, ChartPoint<long, decimal>>> GetCurrentChartPoint(string symbol);
    }
}
