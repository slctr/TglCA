using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Dal.Mock.MockData;

namespace TglCA.Dal.Mock.MockRepositories;

public class MockCurrencyRepository : ICurrencyRepository
{
    private readonly MockDataProvider _provider = new();

    public void Create(Currency entity)
    {
        _provider.Currencies.Add(entity);
    }

    public void Update(Currency entity)
    {
        foreach (var currency in _provider.Currencies.Where(currency => currency.Id == entity.Id))
        {
            currency.Name = entity.Name;
            currency.CurrencyId = entity.CurrencyId;
            currency.Symbol = entity.Symbol;
        }
    }

    public void Delete(Currency entity)
    {
        _provider.Currencies.Remove(entity);
    }

    public Currency GetById(int id)
    {
        return _provider.Currencies.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Currency> GetAll()
    {
        return _provider.Currencies;
    }

    public void CreateOrUpdate(Currency entity)
    {
        if (_provider.Currencies.Contains(entity))
        {
            Update(entity);
            return;
        }

        Create(entity);
    }
}