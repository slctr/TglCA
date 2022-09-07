using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyMapper _currencyMapper;

    public CurrencyService(ICurrencyRepository repository, ICurrencyMapper currencyMapper)
    {
        _currencyRepository = repository;
        _currencyMapper = currencyMapper;
    }

    public void Create(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        _currencyRepository.Create(currency);
    }

    public void Update(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        _currencyRepository.Update(currency);
    }

    public void Delete(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        _currencyRepository.SafeDelete(currency);
    }

    public BllCurrency GetById(int id)
    {
        Currency? currency = _currencyRepository.GetById(id);
        if (currency == null)
        {
            return null;
        }

        BllCurrency bllCurrency = _currencyMapper.ToBllCurrency(currency);

        /*
         Some API calls for bllCurrency
         */

        return bllCurrency;
    }

    public IEnumerable<BllCurrency> GetAll()
    {
        var allCurrencies = _currencyRepository
            .GetAll()
            .Select(c => _currencyMapper.ToBllCurrency(c));

        /*
         Some API calls for bllCurrency
         */

        return allCurrencies;
    }

    public void CreateOrUpdate(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        _currencyRepository.CreateOrUpdate(currency);
    }

    public IEnumerable<BllCurrency> GetAllByMarketCap()
    {
        var allCurrencies = _currencyRepository
            .GetAll()
            .Select(c => _currencyMapper.ToBllCurrency(c))
            .OrderByDescending(c => c.MarketCapUsd);

        /*
         Some API calls for bllCurrency
         */

        return allCurrencies;
    }

    public BllCurrency GetByCurrencyId(string id)
    {
        Currency? currency = _currencyRepository.GetByCurrencyId(id);
        if (currency == null)
        {
            return null;
        }
        BllCurrency bllCurrency = _currencyMapper.ToBllCurrency(currency);

        /*
         Some API calls for bllCurrency
         */

        return bllCurrency;
    }
}
