using eXtensionSharp;
using Microsoft.EntityFrameworkCore;
using SampleConsole.Entities;
using SampleConsole.Services.Abstract;

namespace SampleConsole.Services;

public class TenantService : ITenantService
{
    private readonly AppDbContext _db;
    public TenantService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Tenant> GetAsync(string tenantId)
    {
        return await _db.Tenants.FirstOrDefaultAsync(m => m.TenantId == tenantId);
    }

    public async Task AddAsync(Tenant item)
    {
        var exist = await GetAsync(item.TenantId);
        if (exist.xIsNotEmpty()) throw new Exception("Already exist");

        await _db.Tenants.AddAsync(item);
        await _db.SaveChangesAsync();
    }
}