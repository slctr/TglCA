using TglCA.Bll.Interfaces.Entities.Base;

namespace TglCA.Bll.Interfaces.IServices.Base;

public interface IService<T> where T : BllEntityBase
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T GetById(int id);
    Task<IEnumerable<T>> GetAllAsync();
}