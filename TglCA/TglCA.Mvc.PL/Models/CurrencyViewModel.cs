namespace TglCA.Mvc.PL.Models;

public record CurrencyViewModel
{
    public string Img { get; init; }
    public int Rank { get; init; }
    public string Name { get; init; }
    public string Symbol { get; init; }

    public double Price { get; init; }

    public double PercentChange1h { get; init; }

    public double PercentChange24h { get; init; }

    public double PercentChange7d { get; init; }

    public double Volume24hUsd { get; init; }

    public double MarketCapUsd { get; init; }
}