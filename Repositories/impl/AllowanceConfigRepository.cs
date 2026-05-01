using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Repositories;

public class AllowanceConfigRepository : IAllowanceConfigRepository
{
    private readonly ApplicationDbContext _context;

    public AllowanceConfigRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AllowanceConfig>> GetAllAsync()
    {
        return await _context.AllowanceConfigs.OrderBy(a => a.Name).ToListAsync();
    }

    public async Task<List<AllowanceConfig>> GetActiveAsync()
    {
        return await _context.AllowanceConfigs
            .Where(a => a.Status == AllowanceStatus.Active.ToValue())
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<AllowanceConfig?> GetByIdAsync(Guid id)
    {
        return await _context.AllowanceConfigs.FindAsync(id);
    }

    public async Task<AllowanceConfig?> GetByCodeAsync(string code)
    {
        return await _context.AllowanceConfigs.FirstOrDefaultAsync(a => a.Code == code);
    }

    public async Task AddAsync(AllowanceConfig config)
    {
        await _context.AllowanceConfigs.AddAsync(config);
    }

    public Task UpdateAsync(AllowanceConfig config)
    {
        _context.AllowanceConfigs.Update(config);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
