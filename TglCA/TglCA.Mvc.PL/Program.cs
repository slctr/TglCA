using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TglCA.Dal.Data.DbContextData;
using TglCA.Dal.Interfaces.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

#region Services

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MainDbContext>(options
    => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, Role>(options =>
    {
        #region Password options

        options.Password = builder.Configuration
            .GetSection("AppIdentitySettings:Password")
            .Get<PasswordOptions>();

        #endregion

        #region Lockout options

        options.Lockout = builder.Configuration
            .GetSection("AppIdentitySettings:Lockout")
            .Get<LockoutOptions>();

        #endregion

        #region User options

        options.User = builder.Configuration
            .GetSection("AppIdentitySettings:User")
            .Get<UserOptions>();

        #endregion

        #region SignIn options

        options.SignIn = builder.Configuration
            .GetSection("AppIdentitySettings:SignIn")
            .Get<SignInOptions>();

        #endregion
    })
    .AddEntityFrameworkStores<MainDbContext>();

#endregion


var app = builder.Build();

#region HTTP pipeline

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

#endregion


app.Run();