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
            Symbol = currency.Symbol
        };
    }

    public Currency ToCurrency(BllCurrency bllCurrency)
    {
        return new Currency
        {
            Id = bllCurrency.Id,
            Symbol = bllCurrency.Symbol
        };
    }
}