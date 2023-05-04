namespace TglCA.Mvc.PL.Models
{
    public record CoinViewModel
    {
        public CurrencyViewModel Currency { get; init; }
        public List<MarketViewModel> Markets { get; init; }

    }
}
