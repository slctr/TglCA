using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Mvc.PL.Models;
using TglCA.Mvc.PL.Models.Mappers;
using X.PagedList;

namespace TglCA.Mvc.PL.Controllers;

[Route("/[controller]")]
[Route("/[controller]/[action]")]
public class MainController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ICurrencyService _currencyService;

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

    private IPagedList<CurrencyViewModel> GetPagedViewModel(
        IEnumerable<BllCurrency> currencies,
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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}