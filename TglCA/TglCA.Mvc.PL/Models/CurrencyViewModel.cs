namespace TglCA.Mvc.PL.Models;

public record CurrencyViewModel
{
    public string AssetName { get; init; }
    public string Symbol { get; init; }
    public decimal Price { get; init; }
    public decimal PercentChange24h { get; init; }
    public decimal Volume24hBaseAsset { get; init; }
    public decimal Volume24hUsd { get; init; }
}