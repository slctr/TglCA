using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TglCA.Dal.Interfaces.Entities.Identity;

namespace TglCA.Dal.Data.DbContextData
{
    public class MainDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        #region DbSets

        

        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles");
            
            modelBuilder.Entity<UserClaim>()
                .ToTable("UserClaims");
            
            modelBuilder.Entity<UserLogin>()
                .ToTable("UserLogins");

            modelBuilder.Entity<UserToken>()
                .ToTable("UserToken");

            modelBuilder.Entity<Role>()
                .ToTable("Roles");
            
            modelBuilder.Entity<RoleClaim>()
                .ToTable("RoleClaims");
        }
    }
}
