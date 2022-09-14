using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Mvc.PL.Models
{
    public record MarketViewModel
    {
        public string Name { get; init; }
        public BllCurrency Currency { get; init; }
    }
}
