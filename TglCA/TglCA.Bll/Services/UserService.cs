using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.BllModels;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Bll.Interfaces.Interfaces.EmailService;
using TglCA.Dal.Data.DbContextData;
using TglCA.Dal.Interfaces.Entities.Identity;
using TglCA.Utils;


namespace TglCA.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ICurrencyService _currencyService;
        private readonly MainDbContext _context;


        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ICurrencyService service,
            MainDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _currencyService = service;
            _context = context;
        }

        public async ValueTask<ErrorModel> CreateAsync(BllUserModel userModel)
        {
            var user = new User
            {
                Email = userModel.Email,
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                var errorModel = new ErrorModel()
                {
                    ErrorDetails = result.Errors.Select(er => new ErrorDetail()
                    {
                        ErrorMessage = er.Description
                    })
                };
                return errorModel;
            }

            return new ErrorModel();
        }

        public async ValueTask<ErrorModel> LoginAsync(BllUserModel userModel)
        {
            User user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null)
            {
                var errorModel = new ErrorModel();
                errorModel.ErrorDetails = new List<ErrorDetail>()
                {
                     new ErrorDetail()
                     {
                         ErrorMessage = "SignIn failure: Wrong login info!"
                     }
                };
                return errorModel;
            }

            await _signInManager.SignOutAsync();

            var result =
                await _signInManager.PasswordSignInAsync(user, userModel.Password, false, true);

            if (!result.Succeeded)
            {
                var errorModel = new ErrorModel();
                if (result.IsNotAllowed)
                {
                    errorModel.ErrorDetails = new List<ErrorDetail>()
                    {
                        new ErrorDetail()
                        {
                            ErrorMessage = "SignIn failure: Email is not confirmed"
                        }
                    };
                    return errorModel;
                }
                errorModel.ErrorDetails = new List<ErrorDetail>()
                {
                     new ErrorDetail()
                     {
                         ErrorMessage = "SignIn failure: Wrong login info!"
                     }
                };
                return errorModel;
            }

            return ErrorModel.CreateSuccess();
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async ValueTask<ErrorModel> GoogleResponse()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                var errorModel = new ErrorModel();
                errorModel.ErrorDetails = new List<ErrorDetail>()
                {
                     new ErrorDetail()
                     {
                         ErrorMessage = "SignIn failure: Wrong login info!"
                     }
                };
                return errorModel;
            }

            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                return ErrorModel.CreateSuccess();
            }

            var bllUser = new BllUserModel()
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value
            };

            var user = new User
            {
                Email = bllUser.Email,
                UserName = bllUser.UserName
            };

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {
                var errorModel = new ErrorModel();

                errorModel.ErrorDetails = identityResult.Errors.Select(er => new ErrorDetail()
                {
                    ErrorMessage = er.Description
                });

                return errorModel;
            }

            identityResult = await _userManager.AddLoginAsync(user, info);

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, isPersistent: false);

            return ErrorModel.CreateSuccess();
        }

        public async ValueTask<ErrorModel> CoinSubscribe(string userId, string symbol)
        {
            var user = await _context.Users
                .Include(x => x.Currencies)
                .SingleOrDefaultAsync(x => x.Id == int.Parse(userId)); ;

            if (user == null)
            {
                var errorModel = new ErrorModel();
                errorModel.ErrorDetails = new List<ErrorDetail>()
                {
                     new ErrorDetail()
                     {
                         ErrorMessage = "Something went wrong!"
                     }
                };
                return errorModel;
            }

            user.Currencies = user.Currencies.Any(x => x.Symbol == symbol)
                ? user.Currencies.Where(c => c.Symbol != symbol).ToList()
                : user.Currencies.Append(_currencyService.GetBySymbol(symbol));

            await _userManager.UpdateAsync(user);
            
            return ErrorModel.CreateSuccess();
        }

        public async ValueTask<IEnumerable<BllCurrency>> GetSubscriptions(string userId)
        {
            var user = await _context.Users
                .Include(x => x.Currencies)
                .SingleOrDefaultAsync(x => x.Id == int.Parse(userId));

            if (user == null || user.Currencies == null)
            {
                return null;
            }

            var coins = await _currencyService.GetAllByVolume();

            coins = from x in coins
                    join y in user.Currencies on x.Symbol equals y.Symbol
                    select x;

            return coins;
        }

        public async ValueTask<bool> IsSubscribed(string userId, string symbol)
        {
            var user = await _context.Users
                .Include(x => x.Currencies)
                .SingleOrDefaultAsync(x => x.Id == int.Parse(userId));

            if (user == null || user.Currencies == null)
            {
                return false;
            }

            return user.Currencies.Any(x => x.Symbol == symbol);
        }

        public async ValueTask<string> CreateConfirmationTokenAsync(BllUserModel user)
        {
            User userToConfirm = await _userManager.FindByEmailAsync(user.Email);
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(userToConfirm);
            return Base64EncodeHelper.Base64Encode(token);
        }

        public async ValueTask<IdentityResult> ConfirmEmailByUserNameAsync(string userName, string token)
        {
            User user = await _userManager.FindByNameAsync(userName);
            string decodedToken = Base64EncodeHelper.Base64Decode(token);
            return await _userManager.ConfirmEmailAsync(user, decodedToken);
        }

        public async ValueTask<BllUserModel> GetUserByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            BllUserModel bllUser = new()
            {
                Email = user.Email
            };
            return bllUser;
        }
    }
}