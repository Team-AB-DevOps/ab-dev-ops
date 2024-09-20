using api.Abstractions;

namespace api.Services;

public class WeatherApi : IWeatherApi
{
    private readonly HttpClient _httpClient;
    private readonly string _weatherKey;

    public WeatherApi(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _weatherKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY") ??
                      throw new ArgumentNullException(nameof(_weatherKey));
    }

    public async Task<string?> GetWeatherResponse()
    {
        var response = await _httpClient.GetAsync(
            $"http://api.weatherapi.com/v1/current.json?key={_weatherKey}&q=Copenhagen&aqi=no");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null;
    }
}