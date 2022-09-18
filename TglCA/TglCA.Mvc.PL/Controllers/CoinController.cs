using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Mvc.PL.Models;
using TglCA.Mvc.PL.Models.Mappers;

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
        public async ValueTask<IActionResult> CoinInfo(string id)
        {
            BllCurrency bllCurrency = await _currencyService.GetAverageByMarketId(id);
            if (bllCurrency == null)
            {
                return null;
            }
            CoinViewModel viewModel = new CoinViewModel()
            {
                Currency = bllCurrency.ToViewModel()
            };
            return View(viewModel);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CoinChartInitialValues(string id)
        {
            var points = await _currencyService.GetCurrencyPriceHistory(id);

            return Content(JsonConvert.SerializeObject(points));
        }

        [HttpGet("{id}")]
        public IActionResult CoinChartGetCurrentValue(string id)
        {
            var currentPoints = _currencyService.GetByMarketId(id);
            return Content(JsonConvert.SerializeObject(currentPoints));
        }
    }
}
