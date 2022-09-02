using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Mvc.PL.Models.Mappers;

public static class CurrencyModelMapperExtension
{
    public static CurrencyViewModel ToViewModel(this BllCurrency bllCurrency)
    {
        return new CurrencyViewModel
        {
            Img = bllCurrency.Img,
            MarketCapUsd = bllCurrency.MarketCapUsd,
            Name = bllCurrency.Name,
            PercentChange1h = bllCurrency.PercentChange1h,
            PercentChange24h = bllCurrency.PercentChange24h,
            PercentChange7d = bllCurrency.PercentChange7d,
            Volume24hUsd = bllCurrency.Volume24hUsd,
            Price = bllCurrency.Price,
            Rank = bllCurrency.Rank,
            Symbol = bllCurrency.Symbol
        };
    }

    public static IEnumerable<CurrencyViewModel> ToViewModels
        (this IEnumerable<BllCurrency> currencies)
    {
        return currencies.Select(c => c.ToViewModel());
    }
}