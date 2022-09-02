using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Dal.Mock.MockData
{
    public class MockDataProvider
    {
        private static readonly List<Currency> referenceCurrencies = new List<Currency>
        {
            new Currency()
            {
                Id = 1,
                Name = "Bitcoin",
                Symbol = "BTC",
                CurrencyId = "bitcoin"
            },
            new Currency()
            {
                Id = 2,
                Name = "Ethereum",
                Symbol = "ETH",
                CurrencyId = "ethereum"
            },
            new Currency()
            {
                Id = 3,
                Name = "Tether",
                Symbol = "USDT",
                CurrencyId = "tether"
            },
            new Currency()
            {
                Id = 4,
                Name = "USD Coin",
                Symbol = "USDC",
                CurrencyId = "usd_coin"
            },
            new Currency()
            {
                Id = 5,
                Name = "Binance USD",
                Symbol = "BUSD",
                CurrencyId = "binance_usd"
            }
        };

        public List<Currency> Currencies { get; set; } = GenerateCurrencies(1000);
        private static List<Currency> GenerateCurrencies(int count)
        {
            Random random = new Random();
            List<Currency> currencies = new List<Currency>();
            for (int i = 0; i < count; ++i)
            {
                currencies.Add(referenceCurrencies[random.Next(0,4)]);
            }
            return currencies;
        }
    }
}
