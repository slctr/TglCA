﻿namespace TglCA.Mvc.PL.Models;

public class CurrencyViewModel
{
    private double _marketCapUsd;

    private double _percentageChange1h;
    private double _percentageChange24h;
    private double _percentageChange7d;
    private double _price;
    private double _volume24hUsd;
    public byte[] Img { get; set; }
    public int Rank { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }

    public double Price
    {
        get => RoundToTwo(_price);
        set
        {
            if (value > 0)
                _price = value;
            else
                _price = 0;
        }
    }

    public double PercentChange1h
    {
        get => RoundPercentage(_percentageChange1h);
        set => _percentageChange1h = value;
    }

    public double PercentChange24h
    {
        get => RoundPercentage(_percentageChange24h);
        set => _percentageChange24h = value;
    }

    public double PercentChange7d
    {
        get => RoundPercentage(_percentageChange7d);
        set => _percentageChange7d = value;
    }

    public double Volume24hUsd
    {
        get => RoundToTwo(_volume24hUsd);
        set => _volume24hUsd = value;
    }

    public double MarketCapUsd
    {
        get => RoundToTwo(_marketCapUsd);
        set => _marketCapUsd = value;
    }

    public string GetImageSrc()
    {
        return $"data:image/jpg;base64,{Convert.ToBase64String(Img)}";
    }

    private double RoundPercentage(double input)
    {
        return Math.Round(input, 1);
    }

    private double RoundToTwo(double input)
    {
        return Math.Round(input, 2);
    }
}