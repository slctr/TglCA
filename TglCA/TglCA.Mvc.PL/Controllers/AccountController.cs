using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TglCA.Dal.Interfaces.Entities.Identity;

namespace TglCA.Mvc.PL.Controllers
{
    [Route("/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //Only for TESTING purposes. Should be reworked
        [HttpPost]
        public async Task<IActionResult> Register(User user,string password)
        {
            if (user.Email == null)
            {
                TempData["error"] = "Email is null!!";
                return View();
            }
            if (password == null)
            {
                TempData["error"] = "Password is null!!";
                return View();
            }
            user.UserName = user.Email.Substring(0, user.Email.IndexOf('@'));
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    TempData["error"] += error.Description + "||";
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
