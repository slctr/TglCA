using Microsoft.EntityFrameworkCore;
using TglCA.Dal.Data.DbContextData;
using TglCA.Dal.Interfaces.Entities.Base;
using TglCA.Dal.Interfaces.IRepositories.IBase;

namespace TglCA.Dal.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly MainDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(MainDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
