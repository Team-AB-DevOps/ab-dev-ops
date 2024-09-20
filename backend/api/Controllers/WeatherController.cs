using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class WeatherController : ControllerBase
{
    private readonly WeatherApi _weatherApi;

    public WeatherController(WeatherApi weatherApi)
    {
        _weatherApi = weatherApi;
    }

    [Route("/api/weather")]
    [HttpGet]
    public async Task<IActionResult> GetWeather()
    {
        var result = await _weatherApi.GetWeatherResponse();
        
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}