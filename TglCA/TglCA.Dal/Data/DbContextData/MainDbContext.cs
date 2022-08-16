using Microsoft.EntityFrameworkCore;

namespace TglCA.Dal.Data.DbContextData
{
    internal class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        #region DbSets

        

        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
