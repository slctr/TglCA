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
                Img = SetImg("0xbtc.png")
            },
            new Currency()
            {
                Id = 2,
                Name = "Ethereum",
                Symbol = "ETH",
                Img = SetImg("eth.png")
            },
            new Currency()
            {
                Id = 3,
                Name = "Tether",
                Symbol = "USDT",
                Img = SetImg("usdt.png")
            },
            new Currency()
            {
                Id = 4,
                Name = "USD Coin",
                Symbol = "USDC",
                Img = SetImg("usdc.png")
            },
            new Currency()
            {
                Id = 5,
                Name = "Binance USD",
                Symbol = "BUSD",
                Img = SetImg("bnb.png")
            }
        };

        public List<Currency> Currencies { get; set; } = GenerateCurrencies(1000);

        private static byte[] SetImg(string imgName)
        {
            string imagepath = Directory.GetCurrentDirectory() + $@"\Img\{imgName}";
            using (FileStream fs = new FileStream(imagepath, FileMode.Open))
            {
                byte[] byData = new byte[fs.Length];
                fs.Read(byData, 0, byData.Length);
                return byData;
            }
        }

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
