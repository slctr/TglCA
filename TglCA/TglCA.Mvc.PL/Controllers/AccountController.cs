﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TglCA.Dal.Interfaces.Entities.Identity;
using TglCA.Mvc.PL.Models;

namespace TglCA.Mvc.PL.Controllers;

[Route("/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
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
            Email = userInputModel.Email
        };
        user.UserName = userInputModel.UserName;
        var result = await _userManager.CreateAsync(user, userInputModel.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            return View(userInputModel);
        }

        return RedirectToAction(nameof(Successful));
    }
}