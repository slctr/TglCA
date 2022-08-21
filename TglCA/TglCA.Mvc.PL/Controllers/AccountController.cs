using Microsoft.AspNetCore.Identity;
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
    public async ValueTask<IActionResult> Register(RegistrationModel registrationModel)
    {
        if (!ModelState.IsValid) return View(registrationModel);

        var user = new User
        {
            Email = registrationModel.Email
        };
        user.UserName = user.Email.Substring(0, user.Email.IndexOf('@'));
        var result = await _userManager.CreateAsync(user, registrationModel.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            return View(registrationModel);
        }

        return RedirectToAction(nameof(Successful));
    }
}