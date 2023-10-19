using Microsoft.EntityFrameworkCore;

namespace SampleConsole.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}