
using TglCA.Bll.Interfaces.Entities;
using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Bll.Mappers
{
    internal static class CurrencyMapperExtension
    {
        public static BllCurrency ToBllCurrency(this Currency currency)
        {
            return new BllCurrency
            {
                Id = currency.Id,
                MarketId = currency.MarketId,
                Name = currency.Name,
                Img = currency.Img,
                Symbol = currency.Symbol
            };
        }
        public static Currency ToCurrency(this BllCurrency bllCurrency)
        {
            return new Currency
            {
                Id = bllCurrency.Id,
                MarketId = bllCurrency.MarketId,
                Name = bllCurrency.Name,
                Img = bllCurrency.Img,
                Symbol = bllCurrency.Symbol
            };
        }
    }
}
