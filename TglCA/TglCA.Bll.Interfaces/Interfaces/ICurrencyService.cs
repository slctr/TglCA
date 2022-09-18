using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Bll.Interfaces.IServices.Base;
using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Bll.Interfaces.Interfaces;

public interface ICurrencyService : IService<BllCurrency>
{
    //void CreateOrUpdate(BllCurrency entity);
    Task<IEnumerable<BllCurrency>> GetAllByVolume();
    Task<Dictionary<string, BllCurrency>> GetFromAllMarketsBySymbol(string symbol);
    Task<BllCurrency> GetAverageByMarketId(string currencyId);
    Task<Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>> GetCurrencyPriceHistory(string symbol);
    Task<Dictionary<string, ChartPoint<long, decimal>>> GetLatestPriceHistoryPoint(string symbol);
    // new
    Task UpdateDatabase();
    Currency GetBySymbol(string symbol);
}