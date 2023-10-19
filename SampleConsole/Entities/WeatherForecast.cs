using System.ComponentModel.DataAnnotations.Schema;

namespace SampleConsole.Entities;

[Table("Weatherforecast", Schema = "example")]
public class WeatherForecast
{
    public string TenantId { get; set; }
    public int Id { get; set; }
    public string City { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}