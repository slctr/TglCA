using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.IServices;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(ICurrencyRepository repository)
    {
        _currencyRepository = repository;
    }

    public void Create(BllCurrency entity)
    {
        var currency = new Currency
        {
            Id = entity.Id,
            CurrencyId = entity.CurrencyId,
            Name = entity.Name,
            Symbol = entity.Symbol
        };
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

    public IEnumerable<BllCurrency> GetAll()
    {
        throw new NotImplementedException();
    }

    public void CreateOrUpdate(BllCurrency entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BllCurrency> GetAllByMarketCap()
    {
        throw new NotImplementedException();
    }
}