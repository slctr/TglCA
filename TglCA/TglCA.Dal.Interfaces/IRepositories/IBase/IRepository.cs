using TglCA.Dal.Interfaces.Entities.Base;

namespace TglCA.Dal.Interfaces.IRepositories.IBase;

public interface IRepository<T> where T : BaseEntity, new()
{
    
}