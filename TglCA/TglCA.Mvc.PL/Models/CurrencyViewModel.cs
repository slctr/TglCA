using System.ComponentModel.DataAnnotations;

namespace TglCA.Mvc.PL.Models;

public record CurrencyViewModel
{
    public string Img { get; init; }
    public string AssetName { get; init; }
    public string Symbol { get; init; }
    public decimal Price { get; init; }
    public decimal PercentChange24h { get; init; }
    [Display(Name = "24 Hour Trading Vol")]
    public decimal Volume24hUsd { get; init; }
}