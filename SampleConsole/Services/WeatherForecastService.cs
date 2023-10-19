using eXtensionSharp;
using Microsoft.EntityFrameworkCore;
using SampleConsole.Entities;
using SampleConsole.Services.Abstract;

namespace SampleConsole.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly AppDbContext _db;
    public WeatherForecastService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<WeatherForecast> GetAsync(string tenantId, int id)
    {
        return await _db.WeatherForecasts.FirstOrDefaultAsync(m => m.TenantId == tenantId &&
                                        m.Id == id);
    }

    public async Task<IEnumerable<WeatherForecast>> GetsAsync(string tenantId)
    {
        return await _db.WeatherForecasts.Where(m => m.TenantId == tenantId).ToListAsync();
    }

    public async Task AddAsync(WeatherForecast item)
    {
        var exist = await this.GetAsync(item.TenantId, item.Id);
        if (exist.xIsNotEmpty()) throw new Exception("Already exist");

        await _db.WeatherForecasts.AddAsync(item);
        await _db.SaveChangesAsync();
    }

    public async Task ModifyAsync(WeatherForecast item)
    {
        var exist = await this.GetAsync(item.TenantId, item.Id);
        if (exist.xIsEmpty()) throw new Exception("Item not exist");

        _db.WeatherForecasts.Update(item);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(string tenantId, int id)
    {
        var exist = await this.GetAsync(tenantId, id);
        if(exist.xIsEmpty()) throw new Exception("Item not exist");

        _db.WeatherForecasts.Remove(exist);
        await _db.SaveChangesAsync();
    }
}