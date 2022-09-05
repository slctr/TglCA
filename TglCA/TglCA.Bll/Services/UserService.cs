using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.BllModels;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities.Identity;

namespace TglCA.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            string[] userInfo = {
                info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", string.Empty),
                info.Principal.FindFirst(ClaimTypes.Email).Value
            };
            if (result.Succeeded)
            {
                return ErrorModel.CreateSuccess();
            }

            var user = new User
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", string.Empty)
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

            await _signInManager.SignInAsync(user, isPersistent: false);

            return ErrorModel.CreateSuccess();
        }
    }
}
