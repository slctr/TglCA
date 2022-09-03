using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Bll.Mappers;
using TglCA.Dal.Interfaces.IRepositories;

namespace TglCA.Bll.Services.Mock
{
    public class MockCurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public MockCurrencyService(ICurrencyRepository repository)
        {
            _currencyRepository = repository;
        }
        public void Create(BllCurrency entity)
        {
            _currencyRepository.Create(entity.ToCurrency());
        }

        public void Update(BllCurrency entity)
        {
            _currencyRepository.Update(entity.ToCurrency());
        }

        public void Delete(BllCurrency entity)
        {
            _currencyRepository.Delete(entity.ToCurrency());
        }

        public BllCurrency GetById(int id)
        {
            BllCurrency bllCurrency = _currencyRepository.GetById(id).ToBllCurrency();
            GenerateMockBlValues(bllCurrency);
            return bllCurrency;
        }

        public IEnumerable<BllCurrency> GetAll()
        {
            var currencies = _currencyRepository
                .GetAll()
                .Select(c =>
                {
                    BllCurrency bllCurrency = c.ToBllCurrency();
                    GenerateMockBlValues(bllCurrency);
                    return bllCurrency;
                });
            return currencies;
        }

        public void CreateOrUpdate(BllCurrency entity)
        {
            _currencyRepository.CreateOrUpdate(entity.ToCurrency());
        }

        public IEnumerable<BllCurrency> GetAllByMarketCap()
        {
            var currencies = GetAll();
            return currencies.OrderByDescending(c => c.MarketCapUsd);
        }

        private void GenerateMockBlValues(BllCurrency bllCurrency)
        {
            Random random = new Random();
            bllCurrency.PercentChange1h = GetRandomPercentage();
            bllCurrency.PercentChange24h = GetRandomPercentage();
            bllCurrency.PercentChange7d = GetRandomPercentage();
            bllCurrency.MarketCapUsd = GetRandomDouble(10000);
            bllCurrency.Price = GetRandomDouble(100);
            bllCurrency.Rank = random.Next(1, 1001);
            bllCurrency.Volume24hUsd = GetRandomDouble(1000);

            double GetRandomPercentage()
            {
                var percentageSign = new int[] { -1, 1 };
                return random.NextDouble() * 10 * percentageSign[random.Next(0,2)];
            }

            double GetRandomDouble(int multiply)
            {
                return random.NextDouble() * multiply;
            }
        }

        public BllCurrency GetByMarketId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
