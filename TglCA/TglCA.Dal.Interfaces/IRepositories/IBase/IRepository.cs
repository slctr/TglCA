using TglCA.Dal.Interfaces.Entities.Base;

namespace TglCA.Dal.Interfaces.IRepositories.IBase;

public interface IRepository<T> where T : BaseEntity, new()
{
    Task CreateAsync(T entity);
    Task CreateRange(IEnumerable<T> entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    T? GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAllWithoutQueryFilters();
    Task SafeDeleteAsync(T entity);
}