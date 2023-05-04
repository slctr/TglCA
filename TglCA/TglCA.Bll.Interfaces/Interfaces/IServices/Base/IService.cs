using TglCA.Bll.Interfaces.Entities.Base;

namespace TglCA.Bll.Interfaces.Interfaces.IServices.Base;

public interface IService<T> where T : BllEntityBase
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
}