using System.ComponentModel.DataAnnotations.Schema;

namespace SampleConsole.Entities;

[Table("Tenant", Schema = "example")]
public class Tenant
{
    public string TenantId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string RedirectUrl { get; set; }
}