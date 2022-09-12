using TglCA.Bll.Api.Aggregator.Interfaces;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyMapper _currencyMapper;
    private readonly ICoinAggregator _coinAggregator;

    public CurrencyService(ICurrencyRepository repository, ICoinAggregator aggregator, ICurrencyMapper currencyMapper)
    {
        _currencyRepository = repository;
        _currencyMapper = currencyMapper;
        _coinAggregator = aggregator;
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

    public async Task<IEnumerable<BllCurrency>> GetAllAsync()
    {
        return await _coinAggregator.GetAggregatedCurrencies();
    }

    public void CreateOrUpdate(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        _currencyRepository.CreateOrUpdate(currency);
    }
    public BllCurrency GetByCurrencyId(string currencyId)
    {
        Currency? currency = _currencyRepository.GetByCurrencyId(currencyId);
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

    public async Task<IEnumerable<BllCurrency>> GetAllByVolume()
    {
        var result = await _coinAggregator.GetAggregatedCurrencies();
        return result.OrderByDescending(x => x.Volume24hUsd);
    }

    public async Task<Dictionary<string, BllCurrency>> GetByMarketId(string id)
    {
        return await _coinAggregator.GetAggregatedCurrency(id);
    }
}
