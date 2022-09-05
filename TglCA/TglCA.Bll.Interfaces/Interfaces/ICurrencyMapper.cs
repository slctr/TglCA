using TglCA.Bll.Interfaces.Entities;
using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Bll.Interfaces.Interfaces;

public interface ICurrencyMapper
{
    BllCurrency ToBllCurrency(Currency currency);
    Currency ToCurrency(BllCurrency currency);
}