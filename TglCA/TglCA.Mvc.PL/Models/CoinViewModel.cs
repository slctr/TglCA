namespace TglCA.Mvc.PL.Models
{
    public record CoinViewModel
    {
        public CurrencyViewModel Currency { get; init; }
        public Dictionary<string, double> PriceChart { get; init; }
        public List<MarketViewModel> Markets { get; init; }
        //Max Supply = Theoretical maximum as coded
        public int MaxSupply { get; init; }
        //Total Supply = Onchain supply - burned tokens
        public int TotalSupply { get; init; }
        //The amount of coins that are circulating in the market and are tradeable by the public.
        public int CirculatingSupply { get; init; }
        // FDV = Current Price x Max Supply
        public int FullyDilutedValuation { get; init; }


    }
}
