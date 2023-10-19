// See https://aka.ms/new-console-template for more information

using eXtensionSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleConsole.Entities;
using SampleConsole.Services;
using SampleConsole.Services.Abstract;

var services = new ServiceCollection();
services.AddDbContext<AppDbContext>(sp =>
{
    var con =
        "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EWSDB;Integrated Security=True;MultipleActiveResultSets=True";
    sp.UseSqlServer(con);
});
services.AddScoped<ITenantService, TenantService>();
services.AddScoped<IWeatherForecastService, WeatherForecastService>();

var serviceProvider = services.BuildServiceProvider();
var tenantService = serviceProvider.GetRequiredService<ITenantService>();
var weatherForecastService = serviceProvider.GetRequiredService<IWeatherForecastService>();

var tenantId = "00000";
var tenant = await tenantService.GetAsync(tenantId);
if (tenant.xIsEmpty())
{
    //init
    await tenantService.AddAsync(new Tenant()
    {
        TenantId = tenantId,
        Name = "SYSTEM",
        Region = "KOREA",
        RedirectUrl = "https://example.com"
    });
}

tenant = await tenantService.GetAsync(tenantId);
if (tenant.xIsEmpty()) throw new Exception("Tenant not exist");

var list = await weatherForecastService.GetsAsync(tenantId);
if (list.xIsEmpty())
{
    //init
    var cities = new[] { "Seoul", "Busan", "Gangwon", "Gyggi" };
    var items = new List<WeatherForecast>();
    foreach (var i in Enumerable.Range(1, 100).ToList())
    {
        await weatherForecastService.AddAsync(new WeatherForecast()
        {
            TenantId = tenantId,
            Id = i,
            City = cities[Random.Shared.Next(0, 3)],
            TemperatureC = Random.Shared.Next(-20, 40),
            CreatedOn = DateTime.Now,
            CreatedBy = "SYSTEM",
        }); 
    }
}

list = await weatherForecastService.GetsAsync(tenantId);
if (list.xIsEmpty()) throw new Exception("items is empty");

var selectedItem = list.ToArray()[4];
var exist = await weatherForecastService.GetAsync(selectedItem.TenantId, selectedItem.Id);
if (exist.xIsEmpty()) throw new Exception("item is empty");

exist.TemperatureC = 50;
await weatherForecastService.ModifyAsync(exist);

exist = await weatherForecastService.GetAsync(selectedItem.TenantId, selectedItem.Id);
if (exist.TemperatureC != 50) throw new Exception("not modified");

await weatherForecastService.RemoveAsync(exist.TenantId, exist.Id);
exist = await weatherForecastService.GetAsync(selectedItem.TenantId, selectedItem.Id);
if (exist.xIsNotEmpty()) throw new Exception("item not removed");

Console.WriteLine("Sample application is done.");