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

    public async Task CreateAsync(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        await _currencyRepository.CreateAsync(currency);
    }

    public async Task UpdateAsync(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        await _currencyRepository.UpdateAsync(currency);
    }

    public async Task DeleteAsync(BllCurrency entity)
    {
        Currency currency = _currencyMapper.ToCurrency(entity);
        await _currencyRepository.SafeDeleteAsync(currency);
    }

    public Currency GetBySymbol(string symbol)
    {
        Currency? currency = _currencyRepository.GetBySymbol(symbol);
        if (currency == null)
        {
            return null;
        }
        return currency;
    }

    public async Task<IEnumerable<BllCurrency>> GetAllAsync()
    {
        return await _coinAggregator.GetAggregatedCurrencies();
    }

    public async Task<BllCurrency> GetAverageByMarketId(string currencyId)
    {
        var result = await _coinAggregator.GetBllCurrency(currencyId);
        if (result == null)
        {
            return null;
        }

        return result;
    }

    public async Task<IEnumerable<BllCurrency>> GetAllByVolume()
    {
        var result = await _coinAggregator.GetAggregatedCurrencies();
        if (result == null)
        {
            return null;
        }

        //await UpdateDatabase();

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

    public async Task InitialiseDB()
    {
        var result = await _coinAggregator.GetAggregatedCurrencies();
        var coinList = result.Select(x => _currencyMapper.ToCurrency(x));
        await _currencyRepository.CreateRange(coinList);
    }

    public async Task UpdateDatabase()
    {
        var coins = await _coinAggregator.GetAggregatedCurrencies();
        var currencies = coins.Select(x => _currencyMapper.ToCurrency(x));
        foreach (var c in currencies)
        {
            await _currencyRepository.CreateOrUpdateAsync(c);
        }
    }
}
