using Microsoft.AspNetCore.Mvc;

namespace TglCA.Mvc.PL.Controllers
{
    [Route("/")]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
