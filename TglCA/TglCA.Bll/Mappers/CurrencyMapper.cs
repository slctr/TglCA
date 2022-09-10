using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Bll.Mappers;

public class CurrencyMapper : ICurrencyMapper
{
    public BllCurrency ToBllCurrency(Currency currency)
    {
        return new BllCurrency
        {
            Id = currency.Id,
            Name = currency.Name,
            CurrencyId = currency.CurrencyId,
            Symbol = currency.Symbol,
        };
    }

    public Currency ToCurrency(BllCurrency bllCurrency)
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