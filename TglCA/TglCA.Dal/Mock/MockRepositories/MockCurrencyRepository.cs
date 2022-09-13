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
        Currency? currency = _provider.Currencies
            .FirstOrDefault(currency => currency.Id == entity.Id);
        if (currency == null)
        {
            return;
        }
        currency.Name = entity.Name;
        currency.Symbol = entity.Symbol;
    }

    public void Delete(Currency entity)
    {
        _provider.Currencies.Remove(entity);
    }

    public Currency? GetById(int id)
    {
        return _provider.Currencies
            .Where(c => !c.IsDeleted)
            .FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Currency> GetAll()
    {
        return _provider.Currencies
            .Where(c => !c.IsDeleted);
    }

    public IEnumerable<Currency> GetAllWithoutQueryFilters()
    {
        return _provider.Currencies;
    }

    public void SafeDelete(Currency entity)
    {
        Currency? currency = GetById(entity.Id);
        if (currency == null)
        {
            return;
        }

        currency.IsDeleted = true;
        Update(currency);
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

    public Currency? GetByCurrencyId(string currencyId)
    {
        return _provider.Currencies
            .Where(c => !c.IsDeleted)
            .FirstOrDefault(c => c.Symbol == currencyId);
    }
}