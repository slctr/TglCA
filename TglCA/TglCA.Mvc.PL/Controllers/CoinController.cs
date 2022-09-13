using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TglCA.Bll.Interfaces.Entities.Chart;
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
        public IActionResult CoinInfo(string id)
        {
            // TEST
            CoinViewModel viewModel = new CoinViewModel()
            {
                Currency = _currencyService.GetByCurrencyId(id).ToViewModel()
            };
            return View(viewModel);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CoinChartInitialValues(string id)
        {
            // TEST
            //Dictionary<string, List<ChartPoint<long, double>>> points = new();
            //points.Add("Market1Name", _currencyService.GetCurrencyPriceHistory("Market1Name"));
            //points.Add("Market2Name", _currencyService.GetCurrencyPriceHistory("Market2Name"));
            //points.Add("Market3Name", _currencyService.GetCurrencyPriceHistory("Market3Name"));

            var points = await _currencyService.GetCurrencyPriceHistory(id);

            return Content(JsonConvert.SerializeObject(points));
        }

        [HttpGet("{id}")]
        public IActionResult CoinChartGetCurrentValue(string id)
        {
            // TEST
            var currentPoints = _currencyService.GetByMarketId(id);
            return Content(JsonConvert.SerializeObject(currentPoints));
        }
    }
}
