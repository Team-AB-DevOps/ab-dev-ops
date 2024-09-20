namespace api.Abstractions;

public interface IWeatherApi
{
    Task<string?> GetWeatherResponse();
}
