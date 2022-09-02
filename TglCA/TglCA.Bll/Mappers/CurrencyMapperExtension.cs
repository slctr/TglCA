
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
                Name = currency.Name,
                CurrencyId = currency.CurrencyId,
                Symbol = currency.Symbol
            };
        }
        public static Currency ToCurrency(this BllCurrency bllCurrency)
        {
            return new Currency
            {
                Id = bllCurrency.Id,
                Name = bllCurrency.Name,
                CurrencyId = bllCurrency.CurrencyId,
                Symbol = bllCurrency.Symbol
            };
        }
    }
}
