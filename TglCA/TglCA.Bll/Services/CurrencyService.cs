using TglCA.Bll.Api.Aggregator.Interfaces;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICoinAggregator _coinAggregator;

    public CurrencyService(ICurrencyRepository repository, ICoinAggregator aggregator)
    {
        _currencyRepository = repository;
        _coinAggregator = aggregator;
    }

    public void Create(BllCurrency entity)
    {
        throw new NotImplementedException();
    }

    public void Update(BllCurrency entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(BllCurrency entity)
    {
        throw new NotImplementedException();
    }

    public BllCurrency GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BllCurrency>> GetAllAsync()
    {
        return await _coinAggregator.GetAggregatedCurrencies();
    }

    public void CreateOrUpdate(BllCurrency entity)
    {
        throw new NotImplementedException();
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
