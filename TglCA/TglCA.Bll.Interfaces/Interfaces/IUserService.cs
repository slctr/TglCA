using Microsoft.AspNetCore.Authentication;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.BllModels;

namespace TglCA.Bll.Interfaces.Interfaces
{
    public interface IUserService
    {
        ValueTask<ErrorModel> CreateAsync(BllUserModel userModel);
        ValueTask<ErrorModel> LoginAsync(BllUserModel userModel);
        Task SignOutAsync();
        AuthenticationProperties GetAuthenticationProperties(string provider, string redirectUrl);
        ValueTask<ErrorModel> GoogleResponse();
    }
}
