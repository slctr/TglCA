using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TglCA.Dal.Interfaces.Entities.Identity;
using System.Security.Claims;
using TglCA.Mvc.PL.Models;
using Microsoft.AspNetCore.Authorization;

namespace TglCA.Mvc.PL.Controllers;

[Route("/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;



    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Successful()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(UserInputModel userInputModel)
    {
        if (!ModelState.IsValid) return View(userInputModel);

        var user = new User
        {
            Email = userInputModel.Email,
            UserName = userInputModel.UserName
        };
        var result = await _userManager.CreateAsync(user, userInputModel.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            return View(userInputModel);
        }

        return RedirectToAction(nameof(Successful));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Login(UserInputModel userInputModel)
    {
        if (!ModelState.IsValid) return View(userInputModel);

        User user = await _userManager.FindByEmailAsync(userInputModel.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "SignIn failure: wrong email or password");
            return View(userInputModel);
        }

        await _signInManager.SignOutAsync();

        var result =
            await _signInManager.PasswordSignInAsync(user, userInputModel.Password, false, true);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "SignIn failure: wrong email or password");
            return View(userInputModel);
        }

        return RedirectToAction("Index", "Home");
    }

    public async ValueTask<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult GoogleLogin()
    {
        string redirectUrl = Url.Action("GoogleResponse", "Account");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        return new ChallengeResult("Google", properties);
    }

    [AllowAnonymous]
    public async Task<IActionResult> GoogleResponse()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)

            return RedirectToAction(nameof(Login));

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", string.Empty), info.Principal.FindFirst(ClaimTypes.Email).Value };
        
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        var user = new User
        {
            Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
            UserName = info.Principal.FindFirst(ClaimTypes.Name).Value.Replace(" ", string.Empty)
        };

        var identityResult = await _userManager.CreateAsync(user);

        if (!identityResult.Succeeded)
        {
            return RedirectToAction("Login", "Account");
        }

        identityResult = await _userManager.AddLoginAsync(user, info);

        if (identityResult.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        return RedirectToAction("Index", "Home");

    }

}