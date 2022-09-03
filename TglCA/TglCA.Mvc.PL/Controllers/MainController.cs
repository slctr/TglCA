using Microsoft.AspNetCore.Mvc;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.IRepositories;
using TglCA.Mvc.PL.Models.Mappers;

namespace TglCA.Mvc.PL.Controllers
{

    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public class MainController : Controller
    {
        private ICurrencyService _currencyService;

        public MainController(ICurrencyService service)
        {
            _currencyService = service;
        }
        [Route("/")]
        [HttpGet]
        public IActionResult ByMarketCap()
        {
            return View(_currencyService.GetAllByMarketCap().ToViewModels().ToList());
        }
    }
}
