using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Bll.Interfaces.IServices;
using TglCA.Bll.Mappers;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services.Mock;

public class MockCurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyMapper _currencyMapper;

    public MockCurrencyService(ICurrencyRepository repository, ICurrencyMapper currencyMapper)
    {
        _currencyRepository = repository;
        _currencyMapper = currencyMapper;
    }

    public void Create(BllCurrency entity)
    {
        _currencyRepository.Create(_currencyMapper.ToCurrency(entity));
    }

    public void Update(BllCurrency entity)
    {
        _currencyRepository.Update(_currencyMapper.ToCurrency(entity));
    }

    public void Delete(BllCurrency entity)
    {
        _currencyRepository.Delete(_currencyMapper.ToCurrency(entity));
    }

    public BllCurrency GetById(int id)
    {
        var bllCurrency = _currencyMapper.ToBllCurrency(_currencyRepository.GetById(id));
        GenerateMockBlValues(bllCurrency);
        return bllCurrency;
    }

    public IEnumerable<BllCurrency> GetAll()
    {
        var currencies = _currencyRepository
            .GetAll()
            .Select(c =>
            {
                var bllCurrency = _currencyMapper.ToBllCurrency(c);
                GenerateMockBlValues(bllCurrency);
                return bllCurrency;
            });
        return currencies;
    }

    public void CreateOrUpdate(BllCurrency entity)
    {
        _currencyRepository.CreateOrUpdate(_currencyMapper.ToCurrency(entity));
    }

    public IEnumerable<BllCurrency> GetAllByMarketCap()
    {
        var currencies = GetAll();
        return currencies.OrderByDescending(c => c.MarketCapUsd);
    }

    private void GenerateMockBlValues(BllCurrency bllCurrency)
    {
        var random = new Random();
        bllCurrency.PercentChange1h = GetRandomPercentage();
        bllCurrency.PercentChange24h = GetRandomPercentage();
        bllCurrency.PercentChange7d = GetRandomPercentage();
        bllCurrency.MarketCapUsd = GetRandomDouble(10000);
        bllCurrency.Price = GetRandomDouble(100);
        bllCurrency.Rank = random.Next(1, 1001);
        bllCurrency.Volume24hUsd = GetRandomDouble(1000);

        double GetRandomPercentage()
        {
            var percentageSign = new[] { -1, 1 };
            return random.NextDouble() * 10 * percentageSign[random.Next(0, 2)];
        }

        double GetRandomDouble(int multiply)
        {
            return random.NextDouble() * multiply;
        }
    }
}