using Microsoft.AspNetCore.Mvc;
using TglCA.Bll.Interfaces.Interfaces;

namespace TglCA.Mvc.PL.Controllers
{
    [Route("/[controller]/[action]")]
    public class CoinController : Controller
    {
        private readonly ICurrencyService _currencyService;
        public CoinController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("{Id}")]
        public IActionResult CoinInfo(string id)
        {
            return View();
        }
    }
}
