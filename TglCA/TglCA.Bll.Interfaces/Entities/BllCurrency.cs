using TglCA.Bll.Interfaces.Entities.Base;

namespace TglCA.Bll.Interfaces.Entities;

public class BllCurrency : BllEntityBase
{
    public byte[] Img { get; set; }
    public string CurrencyId { get; set; }
    public int Rank { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public double Price { get; set; }
    public double PercentChange1h { get; set; }
    public double PercentChange24h { get; set; }
    public double PercentChange7d { get; set; }
    public double Volume24hUsd { get; set; }
    public double MarketCapUsd { get; set; }
}