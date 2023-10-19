using SampleConsole.Entities;

namespace SampleConsole.Services.Abstract;

public interface ITenantService
{
    Task<Tenant> GetAsync(string tenantId);
    Task AddAsync(Tenant item);
}