using TglCA.Bll.Interfaces.Entities;

namespace TglCA.Mvc.PL.Models.Mappers;

public static class CurrencyModelMapperExtension
{
    public static CurrencyViewModel ToViewModel(this BllCurrency bllCurrency)
    {
        return new CurrencyViewModel
        {
            Img = bllCurrency.GetImageSrc(),
            AssetName = bllCurrency.AssetName,
            PercentChange24h = bllCurrency.PercentChange24h,
            Volume24hUsd = bllCurrency.Volume24hUsd,
            Price = bllCurrency.Price,
            Symbol = bllCurrency.Symbol
        };
    }

    public static IEnumerable<CurrencyViewModel> ToViewModels(this IEnumerable<BllCurrency> currencies)
    {
        return currencies.Select(c => c.ToViewModel());
    }
}