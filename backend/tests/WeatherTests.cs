using api.Abstractions;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace tests;

public class WeatherTests
{
    private readonly IWeatherApi _weatherApiMock;
    private readonly WeatherController _weatherController;

    public WeatherTests()
    {
        _weatherApiMock = Substitute.For<IWeatherApi>();
        _weatherController = new WeatherController(_weatherApiMock);
    }

    [Fact]
    public async Task Weather_Endpoint_Should_Return_OK()
    {
        // Arrange
        var response = new string("I am weather data!");

        _weatherApiMock.GetWeatherResponse().Returns(response);

        // Act
        var result = await _weatherController.GetWeather();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}