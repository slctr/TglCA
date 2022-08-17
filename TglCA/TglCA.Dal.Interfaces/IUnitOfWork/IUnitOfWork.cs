using Microsoft.EntityFrameworkCore;

namespace TglCA.Dal.Interfaces.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    DbContext Context { get; }

    void Commit();
}