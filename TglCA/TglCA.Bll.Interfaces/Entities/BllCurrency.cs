using TglCA.Bll.Interfaces.Entities.Base;

namespace TglCA.Bll.Interfaces.Entities;

public class BllCurrency : BllEntityBase
{
    private decimal _percentageChange24h;
    private decimal _price;
    private decimal _volume24hUsd;
    public string AssetName { get; set; }
    public string Symbol { get; set; }

    public decimal Price
    {
        get
        {
            if (_price >= 1)
            {
                return Math.Round(_price, 3);
            }
            return Math.Round(_price, 8);
        }
        set
        {
            if (value > 0)
                _price = value;
            else
                _price = 0;
        }
    }

    public decimal PercentChange24h
    {
        get => RoundPercentage(_percentageChange24h);
        set => _percentageChange24h = value;
    }

    public decimal Volume24hUsd
    {
        get => Math.Round(_volume24hUsd, 3);
        set => _volume24hUsd = value;
    }

    private decimal RoundPercentage(decimal input)
    {
        return Math.Round(input, 3);
    }
}