using api.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class WeatherController : ControllerBase
{
	private readonly IWeatherApi _weatherApi;

	public WeatherController(IWeatherApi weatherApi)
	{
		_weatherApi = weatherApi;
	}

	[Route("/api/weather")]
	[HttpGet]
	[ResponseCache(VaryByHeader = "User-Agent", Duration = 1800)]
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
