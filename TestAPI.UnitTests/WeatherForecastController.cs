using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAPI.Database;
using Microsoft.Extensions.Logging;
using Moq;
using TestAPI.Models;
using TestAPI.Services;
using Xunit.Abstractions;

namespace TestAPI.UnitTests;

public class WeatherForecastController
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IWeatherDatabase _weatherDatabase;
    private readonly Mock<IWeatherForecastService> _weatherServiceMock = new Mock<IWeatherForecastService>();
    private readonly Mock<ILogger<Controllers.WeatherForecastController>> _loggerMock = new Mock<ILogger<Controllers.WeatherForecastController>>();
    
    private Controllers.WeatherForecastController _controller;
    private WeatherForecastService _wService;
    
    public WeatherForecastController(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IHostBuilder services = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        var host = services.Build();
        var scope = host.Services.CreateScope();
        _weatherDatabase = scope.ServiceProvider.GetRequiredService<IWeatherDatabase>();
        _weatherDatabase.Migrate();
    }

    // Test if service returns a list of WeatherForecast Objects within a year
    // 1000 tests are made with random numbers generated between 1 and 365
    // Can be removed from loop when testing until failure
    [Fact]
    public async void GetWeatherAsync_ShouldReturnList_WithinAYear()
    {
        _wService = new WeatherForecastService(_weatherDatabase);
        _controller = new Controllers.WeatherForecastController(_loggerMock.Object, _wService);
        
        for (var i = -10; i < 1000; i++)
        {
            // Arrange
            var rand = new Random();
            var days = rand.Next(1,365);
            var token = new CancellationToken(default);
            _testOutputHelper.WriteLine(days.ToString());
        
            // Act
            var weather = await _controller.GetAsync(days, token);

            // Assert
            Assert.NotNull(weather);
            Assert.Equal(typeof(List<WeatherForecast>), weather.GetType());   
        }
    }

    // Test if service returns a valid WeatherForecast Object
    [Fact]
    public async void GetWeatherAsync_ShouldReturnObject_OfTypeWeatherForecast()
    {
        // Arrange
        _wService = new WeatherForecastService(_weatherDatabase);
        _controller = new Controllers.WeatherForecastController(_loggerMock.Object, _wService);
        
        var days = 1;
        var token = new CancellationToken(default);
        _testOutputHelper.WriteLine(days.ToString());
        
        // Act
        var weather = await _controller.GetAsync(days, token);

        // Assert
        Assert.NotNull(weather.First());
        Assert.Equal(typeof(List<WeatherForecast>), weather.GetType());          
    }

    // Test if service returns an empty object with negative days
    [Fact]
    public async void GetWeatherAsync_ShouldReturnEmptyObject_WithNegativeDays()
    {
        // Arrange
        _wService = new WeatherForecastService(_weatherDatabase);
        _controller = new Controllers.WeatherForecastController(_loggerMock.Object, _wService);
        
        var days = -1;
        var token = new CancellationToken(default);
        _testOutputHelper.WriteLine(days.ToString());
        
        // Act
        var weather = await _controller.GetAsync(days, token);

        // Assert
        Assert.NotNull(weather);
        Assert.Equal(typeof(List<WeatherForecast>), weather.GetType()); 
        Assert.Empty(weather);
    }

    // Test if service returns list of WeatherService objects if forecasts are valid
    [Fact]
    public async void GetWeatherAsync_ShouldReturnList_WhenForecastsYieldResults()
    {
        // Arrange
        _wService = new WeatherForecastService(_weatherDatabase);
        _controller = new Controllers.WeatherForecastController(_loggerMock.Object, _wService);
        var days = 5;
        var token = new CancellationToken(default);

        // Act
        var weather = await _controller.GetAsync(days, token);
        
        // Assert
        Assert.True(weather.ToList().Count > 0);
    }
    
    // Test if service returns true if result objects are unique and don't repeat
    [Fact]
    public async void GetWeatherAsync_ShouldReturnTrue_WhenResultsAreUnique()
    {
        // Arrange
        _wService = new WeatherForecastService(_weatherDatabase);
        _controller = new Controllers.WeatherForecastController(_loggerMock.Object, _wService);
        var days = 30;
        var token = new CancellationToken(default);

        // Act
        var weather = await _controller.GetAsync(days, token);
        weather = weather.ToList();
        
        // Assert
        Assert.True(weather.Count() == weather.Distinct().Count());
    }
}