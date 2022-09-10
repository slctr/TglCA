using System.ComponentModel.DataAnnotations;

namespace TglCA.Mvc.PL.Models;

public record CurrencyViewModel
{
    public string CurrencyId { get; init; }
    public string Img { get; init; }
    public int Rank { get; init; }
    public string Name { get; init; }
    public string? Symbol { get; init; }

    public double Price { get; init; }

    public double PercentChange24h { get; init; }
    [Display(Name = "24 Hour Trading Vol")]
    public double Volume24hUsd { get; init; }
}