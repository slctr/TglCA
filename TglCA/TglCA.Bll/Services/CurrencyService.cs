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
    public async Task<BllCurrency> GetAverageByMarketId(string currencyId)
    {
        var result = await _coinAggregator.GetBllCurrency(currencyId);
        if (result == null)
        {
            return null;
        }

        return result; ;
    }

    public async Task<IEnumerable<BllCurrency>> GetAllByVolume()
    {
        var result = await _coinAggregator.GetAggregatedCurrencies();
        if (result == null)
        {
            return null;
        }
        return result.OrderByDescending(x => x.Volume24hUsd);
    }

    public async Task<Dictionary<string, BllCurrency>> GetFromAllMarketsBySymbol(string symbol)
    {
        var result = await _coinAggregator.GetAggregatedCurrency(symbol);
        
        return result;
    }

    public async Task<Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>> GetCurrencyPriceHistory(string currencyId)
    {
        var result = await _coinAggregator.GetAggregatedChart(currencyId);
        if (result == null)
        {
            return null;
        }
        return result.Where(r => r.Value != null && r.Value.Any())
            .ToDictionary(r => r.Key, r => r.Value);
    }

    public async Task<Dictionary<string, ChartPoint<long, decimal>>> GetLatestPriceHistoryPoint(string currencyId)
    {
        var result = await _coinAggregator.GetCurrentChartPoint(currencyId);
        if (result == null)
        {
            return null;
        }
        return result;
    }
}
