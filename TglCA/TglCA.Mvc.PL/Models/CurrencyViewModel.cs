namespace TglCA.Mvc.PL.Models
{
    public record CurrencyViewModel
    {
        public string Img { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        private double Price;

        public double PercentChange1h { get; init; }

        public double PercentChange24h { get; init; }

        public double PercentChange7d { get; init; }

        public double Volume24hUsd { get; init; }

        public double MarketCapUsd { get; init; }
    }
}
