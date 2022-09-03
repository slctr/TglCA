using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TglCA.Bll.Interfaces.Entities.BllModels;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities.Identity;
using TglCA.Mvc.PL.Models;

namespace TglCA.Mvc.PL.Controllers;

[Route("/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userUservice)
    {
        _userService = userUservice;
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
        if (!ModelState.IsValid)
        {
            return View(userInputModel);
        }

        var errModel = await _userService.CreateAsync(new BllUserModel()
        {
            Email = userInputModel.Email,
            Password = userInputModel.Password
        });

        if (!errModel.IsSuccess)
        {
            foreach (var item in errModel.ErrorDetails)
            {
                ModelState.AddModelError(string.Empty, item.ErrorMessage);
            }
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

        var errModel = await _userService.LoginAsync(new BllUserModel()
        {
            Email = userInputModel.Email,
            Password = userInputModel.Password
        });

        if (!errModel.IsSuccess)
        {
            foreach (var item in errModel.ErrorDetails)
            {
                ModelState.AddModelError(string.Empty, item.ErrorMessage);
            }
            return View(userInputModel);
        }

        return RedirectToAction("ByMarketCap", "Main");
    }

    public async ValueTask<IActionResult> LogOut()
    {
        await _userService.SignOutAsync();
        return RedirectToAction("ByMarketCap", "Main");
    }

    [AllowAnonymous]
    public IActionResult GoogleLogin()
    {
        string redirectUrl = Url.Action("GoogleResponse", "Account");
        var properties = _userService.GetAuthenticationProperties("Google", redirectUrl);
        return new ChallengeResult("Google", properties);
    }

    [AllowAnonymous]
    public async ValueTask<IActionResult> GoogleResponse()
    {
        var errModel = await _userService.GoogleResponse();

        if (!errModel.IsSuccess)
        {
            foreach (var item in errModel.ErrorDetails)
            {
                ModelState.AddModelError(string.Empty, item.ErrorMessage);
            }

            return RedirectToAction("Login", "Account");
        }

        return RedirectToAction("ByMarketCap", "Main");
    }
}