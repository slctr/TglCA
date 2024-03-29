﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
        ValueTask<ErrorModel> CoinSubscribe(string userId, string symbol);
        ValueTask<IEnumerable<BllCurrency>> GetSubscriptions(string userId);
        ValueTask<bool> IsSubscribed(string userId, string symbol);
        ValueTask<string> CreateConfirmationTokenAsync(BllUserModel user);
        ValueTask<IdentityResult> ConfirmEmailByUserNameAsync(string userName, string token);
        ValueTask<BllUserModel> GetUserByName(string name);
    }
}
