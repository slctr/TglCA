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
                return Math.Round(_price, 2);
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
        get => Math.Round(_volume24hUsd, 2);
        set => _volume24hUsd = value;
    }

    public string GetImageSrc()
    {
        if (!File.Exists(Environment.CurrentDirectory + $@"\wwwroot\icons\32\color\{Symbol.ToLower()}.png"))
        {
            return "../../icons/32/color/generic.png";
        }
        return $"../../icons/32/color/{Symbol.ToLower()}.png";
    }

    private decimal RoundPercentage(decimal input)
    {
        return Math.Round(input, 5);
    }
}