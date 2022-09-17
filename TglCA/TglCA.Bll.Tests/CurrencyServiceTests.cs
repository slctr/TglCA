using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TglCA.Bll.Api.Aggregator.Interfaces;
using TglCA.Bll.Interfaces.Entities;
using TglCA.Bll.Interfaces.Entities.Chart;
using TglCA.Bll.Interfaces.Interfaces;
using TglCA.Bll.Mappers;
using TglCA.Bll.Services;
using Xunit;

namespace TglCA.Bll.Tests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async void GetAllAsyncTest_AnyTrue()
        {
            //Arrange
            
            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().Returns(new List<BllCurrency>
            {
                new()
                {
                    Id = 1,
                    AssetName = "AssetName1",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST1",
                    Volume24hUsd = 1.2m
                },
                new()
                {
                    Id = 2,
                    AssetName = "AssetName2",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST2",
                    Volume24hUsd = 1.2m
                },
                new()
                {
                    Id = 3,
                    AssetName = "AssetName3",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST3",
                    Volume24hUsd = 1.2m
                },
                new()
                {
                    Id = 4,
                    AssetName = "AssetName4",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST4",
                    Volume24hUsd = 1.2m
                }
            });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());
            
            //Act
            
            var result = await currencyService.GetAllAsync();
            
            //Assert

            result.Should()
                .NotBeEmpty();
        }
        [Fact]
        public async void GetAllAsyncTest_AnyFalseNull()
        {
            //Arrange
            
            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().ReturnsNull();
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());
            
            //Act

            var result = await currencyService.GetAllAsync();

            //Assert

            result.Should()
                .BeNull();
        }
        [Fact]
        public async void GetAllAsyncTest_AnyFalse()
        {
            //Arrange
            
            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().Returns(new List<BllCurrency>());
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());
            
            //Act

            var result = await currencyService.GetAllAsync();

            //Assert

            result.Should()
                .BeEmpty();
        }
        [Fact]
        public async void GetAllByVolumeTest_AnyTrue()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().Returns(new List<BllCurrency>
            {
                new()
                {
                    Id = 1,
                    AssetName = "AssetName1",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST1",
                    Volume24hUsd = 1.2m
                },
                new()
                {
                    Id = 2,
                    AssetName = "AssetName2",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST2",
                    Volume24hUsd = 1.2m
                },
                new()
                {
                    Id = 3,
                    AssetName = "AssetName3",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST3",
                    Volume24hUsd = 1.2m
                },
                new()
                {
                    Id = 4,
                    AssetName = "AssetName4",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST4",
                    Volume24hUsd = 1.2m
                }
            });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetAllByVolume();

            //Assert

            result.Should()
                .NotBeEmpty();
        }
        [Fact]
        public async void GetAllByVolumeTest_AnyFalseNull()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().ReturnsNull();
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetAllByVolume();

            //Assert

            result.Should()
                .BeNull();
        }
        [Fact]
        public async void GetAllByVolumeTest_AnyFalse()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().Returns(new List<BllCurrency>());
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetAllByVolume();

            //Assert

            result.Should()
                .BeEmpty();
        }
        [Fact]
        public async void GetAllByVolumeTest_OrderedTrue()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrencies().Returns(new List<BllCurrency>
            {
                new()
                {
                    Id = 1,
                    AssetName = "AssetName1",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST1",
                    Volume24hUsd = 0.2m
                },
                new()
                {
                    Id = 2,
                    AssetName = "AssetName2",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST2",
                    Volume24hUsd = 111.6m
                },
                new()
                {
                    Id = 3,
                    AssetName = "AssetName3",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST3",
                    Volume24hUsd = 11.8m
                },
                new()
                {
                    Id = 4,
                    AssetName = "AssetName4",
                    PercentChange24h = 1.0m,
                    Price = 1.1m,
                    Symbol = "AST4",
                    Volume24hUsd = 5.3m
                }
            });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetAllByVolume();

            //Assert

            result.Should()
                .BeInDescendingOrder(c => c.Volume24hUsd);
        }
        [Fact]
        public async void GetFromAllMarketsBySymbolTest_AnyTrue()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrency(Arg.Any<string>())
                .Returns(new Dictionary<string, BllCurrency>
                {
                    {
                        "Market1", new BllCurrency()
                        {
                            Id = 1,
                            AssetName = "AssetName1",
                            PercentChange24h = 1.0m,
                            Price = 1.1m,
                            Symbol = "SMB1",
                            Volume24hUsd = 0.2m
                        }
                    },
                    {
                        "Market2", new BllCurrency()
                        {
                            Id = 1,
                            AssetName = "AssetName1",
                            PercentChange24h = 1.8m,
                            Price = 1.1m,
                            Symbol = "SMB1",
                            Volume24hUsd = 1.2m
                        }
                    },
                    {
                        "Market3", new BllCurrency()
                        {
                            Id = 1,
                            AssetName = "AssetName1",
                            PercentChange24h = 1.5m,
                            Price = 1.1m,
                            Symbol = "SMB1",
                            Volume24hUsd = 2.2m
                        }
                    },
                });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetFromAllMarketsBySymbol("SMB1");

            //Assert

            result.Should()
                .NotBeEmpty();
        }
        [Fact]
        public async void GetFromAllMarketsBySymbolTest_AnyFalseNull()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrency(Arg.Any<string>())
                .ReturnsNull();
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetFromAllMarketsBySymbol("SMB1");

            //Assert

            result.Should()
                .BeNull();
        }
        [Fact]
        public async void GetFromAllMarketsBySymbolTest_AnyFalse()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedCurrency(Arg.Any<string>())
                .Returns(new Dictionary<string, BllCurrency>());
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetFromAllMarketsBySymbol("SMB1");

            //Assert

            result.Should()
                .BeEmpty();
        }
        [Fact]
        public async void GetCurrencyPriceHistoryTest_AnyTrue()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedChart(Arg.Any<string>())
                .Returns(new Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>
                {
                    {
                        "Market1",
                        new List<ChartPoint<long,decimal>>
                        {
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                        }
                    },
                    {
                        "Market2",
                        new List<ChartPoint<long,decimal>>
                        {
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                        }
                    },
                    {
                        "Market3",
                        new List<ChartPoint<long,decimal>>
                        {
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                        }
                    }

                });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetCurrencyPriceHistory("SMB1");

            //Assert

            result.Should()
                .NotBeEmpty();
        }
        [Fact]
        public async void GetCurrencyPriceHistoryTest_AnyFalseNull()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedChart(Arg.Any<string>())
                .ReturnsNull();
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetCurrencyPriceHistory("SMB1");

            //Assert

            result.Should()
                .BeNull();
        }
        [Fact]
        public async void GetCurrencyPriceHistoryTest_AnyFalse()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedChart(Arg.Any<string>())
                .Returns(new Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>());
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetCurrencyPriceHistory("SMB1");

            //Assert

            result.Should()
                .BeEmpty();
        }
        [Fact]
        public async void GetCurrencyPriceHistoryTest_NotHaveNullValues()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedChart(Arg.Any<string>())
                .Returns(new Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>
                {
                    {
                        "Market1",
                        new List<ChartPoint<long,decimal>>
                        {
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                        }
                    },
                    {
                        "Market2",
                        null
                    },
                    {
                        "Market3",
                        new List<ChartPoint<long,decimal>>
                        {
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                        }
                    }

                });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetCurrencyPriceHistory("SMB1");

            //Assert

            result.Should().NotContainNulls(d => d.Value);
        }
        [Fact]
        public async void GetCurrencyPriceHistoryTest_NotHaveEmptyAndNullValues()
        {
            //Arrange

            ICoinAggregator coinAggregator = Substitute.For<ICoinAggregator>();
            coinAggregator.GetAggregatedChart(Arg.Any<string>())
                .Returns(new Dictionary<string, IEnumerable<ChartPoint<long, decimal>>>
                {
                    {
                        "Market1",
                        new List<ChartPoint<long,decimal>>()
                    },
                    {
                        "Market2",
                        null
                    },
                    {
                        "Market3",
                        new List<ChartPoint<long,decimal>>
                        {
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                            new (1341234342L, 1.0m),
                        }
                    }

                });
            ICurrencyService currencyService = new CurrencyService(null, coinAggregator, new CurrencyMapper());

            //Act

            var result = await currencyService.GetCurrencyPriceHistory("SMB1");

            //Assert

            result.Should().HaveCount(1);
        }
    }
}