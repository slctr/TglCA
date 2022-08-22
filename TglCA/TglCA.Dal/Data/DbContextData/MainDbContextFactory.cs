using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TglCA.Dal.Data.DbContextData
{
    internal class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        private readonly IConfiguration _configuration = GetConfiguration();
        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()+ @"\Data\DbContextData\")
                .AddJsonFile("designDbContextConfig.json")
                .Build();
        }
        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();

            var connectionString = _configuration.GetConnectionString("Default");

            optionsBuilder.UseSqlServer(connectionString);
            return new MainDbContext(optionsBuilder.Options);
        }
    }
}
