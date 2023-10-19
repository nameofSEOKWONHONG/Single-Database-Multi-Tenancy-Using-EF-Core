using SampleConsole.Entities;

namespace SampleConsole.Services.Abstract;

public interface IWeatherForecastService
{
    Task<WeatherForecast> GetAsync(string tenantId, int id);
    Task<IEnumerable<WeatherForecast>> GetsAsync(string tenantId);
    Task AddAsync(WeatherForecast item);
    Task ModifyAsync(WeatherForecast item);
    Task RemoveAsync(string tenantId, int id);
}