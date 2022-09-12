﻿using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Bll.Interfaces.IServices.Base;

namespace TglCA.Bll.Interfaces.Interfaces;

public interface ICurrencyService : IService<BllCurrency>
{
    void CreateOrUpdate(BllCurrency entity);
    Task<IEnumerable<BllCurrency>> GetAllByVolume();
    Task<Dictionary<string, BllCurrency>> GetByMarketId(string symbol);
    BllCurrency GetByCurrencyId(string currencyId);
    //TEST
    //List<ChartPoint<long, double>> GetCurrencyPriceHistory(string currencyId);
    //ChartPoint<long, double> GetLatestPriceHistoryPoint(string currencyId);
}