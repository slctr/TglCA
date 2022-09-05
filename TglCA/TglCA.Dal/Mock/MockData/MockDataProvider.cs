using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TglCA.Dal.Interfaces.Entities;

namespace TglCA.Dal.Mock.MockData
{
    public class MockDataProvider
    {
        public List<Currency> Currencies { get; set; } = new List<Currency>()
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

        private static byte[] SetImg(string imgName)
        {
            string imagepath = Directory.GetCurrentDirectory() + $@"\Img\{imgName}";
            using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
            {
                byte[] byData = new byte[fileStream.Length];
                fileStream.Read(byData, 0, byData.Length);
                return byData;
            }
        }
    }
}
