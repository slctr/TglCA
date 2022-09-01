using TglCA.Bll.Interfaces.Entities.Base;

namespace TglCA.Bll.Interfaces.IServices.Base;

public interface IService<T> where T : BllEntityBase
{
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public T GetById(int id);
    public IEnumerable<T> GetAll();
}