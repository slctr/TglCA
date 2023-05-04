using TglCA.Dal.Data.DbContextData;
using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Dal.Repositories.Base;

namespace TglCA.Dal.Repositories;

public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
{
    public CurrencyRepository(MainDbContext context) : base(context)
    {
    }
    public async Task CreateOrUpdateAsync(Currency entity)
    {
        Currency? currency = _dbSet.FirstOrDefault(c => c.Symbol == entity.Symbol);
        if (currency == null)
        {
            await CreateAsync(entity);
            return;
        }
    }

    public Currency? GetBySymbol(string symbol)
    {
        return _dbSet.FirstOrDefault(c => c.Symbol == symbol);
    }

}