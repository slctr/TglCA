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

        [HttpGet("{symbol}")]
        public async ValueTask<IActionResult> CoinInfo(string symbol)
        {
            BllCurrency bllCurrency = await _currencyService.GetAverageByMarketId(symbol);
            if (bllCurrency == null)
            {
                return NotFound();
            }

            var currencyByMarket = await _currencyService.GetFromAllMarketsBySymbol(symbol);
            List<MarketViewModel> markets = new List<MarketViewModel>();
            foreach (var marketCurrency in currencyByMarket)
            {
                if (marketCurrency.Value != null)
                {
                    markets.Add(new MarketViewModel
                    {
                        Currency = marketCurrency.Value,
                        Name = marketCurrency.Key
                    });
                }
            }
            CoinViewModel viewModel = new CoinViewModel()
            {
                Currency = bllCurrency.ToViewModel(),
                Markets = markets.OrderByDescending(m => m.Currency.Volume24hUsd).ToList()
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
        public async Task<IActionResult> CoinChartGetCurrentValue(string id)
        {
            // TEST
            var currentPoints = await _currencyService.GetLatestPriceHistoryPoint(id);
            return Content(JsonConvert.SerializeObject(currentPoints));
        }
    }
}
