using TglCA.Dal.Interfaces.Entities.Base;

namespace TglCA.Dal.Interfaces.IRepositories.IBase;

public interface IRepository<T> where T : BaseEntity, new()
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? GetById(int id);
    IEnumerable<T> GetAll();
}