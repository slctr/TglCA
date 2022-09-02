using Microsoft.AspNetCore.Mvc;
using TglCA.Bll.Interfaces.IServices;
using TglCA.Mvc.PL.Models.Mappers;
using X.PagedList;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Mvc.PL.Models;

namespace TglCA.Mvc.PL.Controllers
{
    
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public class MainController : Controller
    {
        private ICurrencyService _currencyService;
        private IConfiguration _configuration;

        public MainController(ICurrencyService service, IConfiguration configuration)
        {
            _currencyService = service;
            _configuration = configuration;
        }
        [Route("/")]
        [HttpGet("{pageNumber}")]
        public IActionResult ByMarketCap(int? page)
        {
            var currencies = _currencyService.GetAllByMarketCap();
            var pageSize = page ?? 1;
            return View(GetPagedViewModel(currencies, pageSize, GetPageSize()));
        }

        private IPagedList<CurrencyViewModel> GetPagedViewModel
            (IEnumerable<BllCurrency> currencies,
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
    }
}
