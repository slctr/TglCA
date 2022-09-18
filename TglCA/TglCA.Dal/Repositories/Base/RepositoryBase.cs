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
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IEnumerable<T> GetAllWithoutQueryFilters()
        {
            return _dbSet.IgnoreQueryFilters();
        }

        public async Task SafeDeleteAsync(T entity)
        {
            T? tEntity = GetById(entity.Id);
            if (tEntity == null)
            {
                return;
            }
            tEntity.IsDeleted = true;
            await UpdateAsync(tEntity);
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
