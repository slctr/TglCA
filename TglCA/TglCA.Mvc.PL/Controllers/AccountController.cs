using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.BllModels;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Mvc.PL.Models;
using TglCA.Mvc.PL.Models.Mappers;
using X.PagedList;

namespace TglCA.Mvc.PL.Controllers;

[Route("/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AccountController(IUserService userUservice, IConfiguration configuration)
    {
        _userService = userUservice;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Successful()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Error()
    {
        ViewBag.Errors = TempData["Errors"];
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
            AddModelStateErrors(errModel);
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
            AddModelStateErrors(errModel);
            return View(userInputModel);
        }

        return RedirectToAction("All", "Main");
    }

    public async ValueTask<IActionResult> LogOut()
    {
        await _userService.SignOutAsync();
        return RedirectToAction("All", "Main");
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
            TempData["Errors"] = errModel.ErrorDetails
                .Select(e => e.ErrorMessage)
                .ToList();
            return RedirectToAction(nameof(Error));
        }

        return RedirectToAction("All", "Main");
    }

    public async Task<IActionResult> Subscribe(string symbol)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var errModel = await _userService.CoinSubscribe(userId, symbol);

        if (!errModel.IsSuccess)
        {
            TempData["Errors"] = errModel.ErrorDetails
                .Select(e => e.ErrorMessage)
                .ToList();
            return RedirectToAction(nameof(Error));
        }

        return RedirectToAction("CoinInfo", "Coin", new {id = symbol});
    }

    [Route("/Account/Subscriptions")]
    [HttpGet("{pageNumber}")]
    public async Task<IActionResult> Subscriptions(int? page)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var userCurrencies = await _userService.GetSubscriptions(userId);
        var pageSize = page ?? 1;
        return View(GetPagedViewModel(userCurrencies, pageSize, GetPageSize()));
    }

    private IPagedList<CurrencyViewModel> GetPagedViewModel(
        IEnumerable<BllCurrency> currencies,
        int pageNumber,
        int pageSize)
    {
        return currencies
            .ToViewModels()
            .ToPagedList(pageNumber, pageSize);
    }

    private int GetPageSize()
    {
        return _configuration.GetSection("PageSettings:PageSize").Get<int>();
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> IsSubscribed(string symbol)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        var result = await _userService.IsSubscribed(userId, symbol);

        return Json(new {status = result});
    }

    private void AddModelStateErrors(ErrorModel errorModel)
    {
        foreach (var item in errorModel.ErrorDetails)
        {
            ModelState.AddModelError(string.Empty, item.ErrorMessage);
        }
    }
}