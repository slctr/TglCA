using TglCA.Dal.Interfaces.Entities;
using TglCA.Dal.Interfaces.IRepositories.IBase;

namespace TglCA.Dal.Interfaces.IRepositories;

public interface ICurrencyRepository : IRepository<Currency>
{
    Task CreateOrUpdateAsync(Currency entity);
    Currency? GetBySymbol(string currencyId);
}