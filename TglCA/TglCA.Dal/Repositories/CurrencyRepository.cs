using TglCA.Dal.Data.DbContextData;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Dal.Repositories.Base;

namespace TglCA.Dal.Repositories;

public class CurrencyRepository : RepositoryBase<Currency>,ICurrencyRepository
{
    public CurrencyRepository(MainDbContext context) : base(context)
    {
    }
    public void CreateOrUpdate(Currency entity)
    {
        Currency? currency = _dbSet.FirstOrDefault(c => c.CurrencyId == entity.CurrencyId);
        if (currency == null)
        {
            Create(entity);
            return;
        }
        Update(entity);
    }

    public Currency? GetByCurrencyId(string currencyId)
    {
        return _dbSet.FirstOrDefault(c => c.CurrencyId == currencyId);
    }
}