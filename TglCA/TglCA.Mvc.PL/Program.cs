using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Bll.Services;
using TglCA.Bll.Services.Mock;
using TglCA.Dal.Data.DbContextData;
using TglCA.Dal.Interfaces.Entities.Identity;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Dal.Mock.MockRepositories;

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
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
builder.Services.AddTransient<ICurrencyRepository, MockCurrencyRepository>();
builder.Services.AddTransient<ICurrencyService, MockCurrencyService>();
builder.Services.AddTransient<IUserService, UserService>();

#endregion


var app = builder.Build();

#region HTTP pipeline

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

#endregion


app.Run();