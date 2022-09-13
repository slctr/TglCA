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
        },
        new()
        {
            Id = 2,
            Name = "Ethereum",
            Symbol = "ETH",
        },
        new()
        {
            Id = 3,
            Name = "Tether",
            Symbol = "USDT",
        },
        new()
        {
            Id = 4,
            Name = "USD Coin",
            Symbol = "USDC",
        },
        new()
        {
            Id = 5,
            Name = "Binance USD",
            Symbol = "BUSD",
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
}