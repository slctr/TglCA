using System.ComponentModel.DataAnnotations;
using TglCA.Bll.Interfaces.Entities.Chart;

namespace TglCA.Mvc.PL.Models
{
    public record CoinViewModel
    {
        public CurrencyViewModel Currency { get; init; }
        public Dictionary<MarketViewModel, List<ChartPoint<long,double>>> PriceChart { get; init; }
        
    }
}
