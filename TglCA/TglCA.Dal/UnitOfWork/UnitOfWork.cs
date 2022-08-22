using Microsoft.EntityFrameworkCore;
using TglCA.Dal.Interfaces.IUnitOfWork;

namespace TglCA.Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            if (Context == null)
                throw new NullReferenceException(nameof(Context));

            try
            {
                Context.SaveChanges();
            }
            // Temporary catch
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            if (Context == null)
                throw new NullReferenceException(nameof(Context));
            
            Context.Dispose();
            
            Context = null;
        }
    }
}
