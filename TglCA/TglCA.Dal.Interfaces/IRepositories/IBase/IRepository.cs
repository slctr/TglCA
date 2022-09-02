using TglCA.Dal.Interfaces.Entities.Base;

namespace TglCA.Dal.Interfaces.IRepositories.IBase;

public interface IRepository<T> where T : BaseEntity, new()
{
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public T GetById(int id);
    public IEnumerable<T> GetAll();
}