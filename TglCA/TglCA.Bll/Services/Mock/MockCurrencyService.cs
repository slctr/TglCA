using TglCA.Bll.Helpers;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Dal.Interfaces.Entities;
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

    public BllCurrency GetByCurrencyId(string currencyId)
    {
        Currency? currency = _currencyRepository.GetByCurrencyId(currencyId);
        if (currency == null)
        {
            return null;
        }

        BllCurrency bllCurrency = _currencyMapper.ToBllCurrency(currency);
        GenerateMockBlValues(bllCurrency);
        return bllCurrency;
    }

    public List<ChartPoint<long, double>> GetCurrencyPriceHistory(string currencyId)
    {
        List<ChartPoint<long, double>> points = new List<ChartPoint<long, double>>();
        Random random = new Random();
        DateTime initialDate = DateTime.Today.AddMonths(-1);
        while (initialDate != DateTime.Today)
        {
            points.Add(new ChartPoint<long, double>(initialDate.ToUnixTimestamp(),random.NextDouble()*1000));
            initialDate = initialDate.AddDays(1);
        }
        return points;
    }

    public ChartPoint<long, double> GetLatestPriceHistoryPoint(string currencyId)
    {
        Random random = new Random();
        ChartPoint<long, double> point = new(DateTimeHelper.UnixTimestampNow(), random.NextDouble() * 1000);
        return point;
    }

    private void GenerateMockBlValues(BllCurrency bllCurrency)
    {
        var random = new Random();
        bllCurrency.PercentChange24h = GetRandomPercentage();
        bllCurrency.Price = GetRandomDouble(100);
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