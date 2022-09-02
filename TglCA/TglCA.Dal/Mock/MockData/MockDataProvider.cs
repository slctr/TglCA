using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Dal.Mock.MockData;

public class MockDataProvider
{
    private static readonly List<Currency> referenceCurrencies = new()
    {
        new()
        {
            Id = 1,
            Name = "Bitcoin",
            Symbol = "BTC",
            CurrencyId = "bitcoin",
            Img = SetImg("bitcoin.png")
        },
        new()
        {
            Id = 2,
            Name = "Ethereum",
            Symbol = "ETH",
            CurrencyId = "ethereum",
            Img = SetImg("ethereum.png")
        },
        new()
        {
            Id = 3,
            Name = "Tether",
            Symbol = "USDT",
            CurrencyId = "tether",
            Img = SetImg("tether.png")
        },
        new()
        {
            Id = 4,
            Name = "USD Coin",
            Symbol = "USDC",
            CurrencyId = "usd_coin",
            Img = SetImg("usd_coin.png")
        },
        new()
        {
            Id = 5,
            Name = "Binance USD",
            Symbol = "BUSD",
            CurrencyId = "binance_usd",
            Img = SetImg("binance_usd.png")
        }
    };

    public List<Currency> Currencies { get; set; } = GenerateCurrencies(1000);

    private static List<Currency> GenerateCurrencies(int count)
    {
        var random = new Random();
        var currencies = new List<Currency>();
        for (var i = 0; i < count; ++i) currencies.Add(referenceCurrencies[random.Next(0, 5)]);
        return currencies;
    }

    private static byte[] SetImg(string imgName)
    {
        var imagepath = Directory.GetCurrentDirectory() + $@"\Img\{imgName}";
        using (var fs = new FileStream(imagepath, FileMode.Open))
        {
            var byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            return byData;
        }
    }
}