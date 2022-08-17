using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TglCA.Dal.Data.DbContextData
{
    internal class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();

            //Must be updated
            var connectionString =
                @"Server=localhost; Database=TglCA_Db; TrustServerCertificate=True; Trusted_Connection=true";

            optionsBuilder.UseSqlServer(connectionString);
            return new MainDbContext(optionsBuilder.Options);
        }
    }
}
