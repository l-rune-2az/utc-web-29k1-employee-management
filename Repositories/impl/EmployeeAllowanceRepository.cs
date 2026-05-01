using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public class EmployeeAllowanceRepository : IEmployeeAllowanceRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeAllowanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeAllowance>> GetAllAsync()
    {
        return await _context.EmployeeAllowances
            .Include(a => a.Employee)
            .Include(a => a.Contract)
            .Include(a => a.AllowanceConfig)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<EmployeeAllowance>> GetByEmployeeIdAsync(Guid empId)
    {
        return await _context.EmployeeAllowances
            .Include(a => a.Contract)
            .Include(a => a.AllowanceConfig)
            .Where(a => a.EmpId == empId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<EmployeeAllowance>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.EmployeeAllowances
            .Include(a => a.AllowanceConfig)
            .Where(a => a.ContractId == contractId)
            .ToListAsync();
    }

    public async Task<EmployeeAllowance?> GetByIdAsync(Guid id)
    {
        return await _context.EmployeeAllowances
            .Include(a => a.Employee)
            .Include(a => a.Contract)
            .Include(a => a.AllowanceConfig)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(EmployeeAllowance allowance)
    {
        await _context.EmployeeAllowances.AddAsync(allowance);
    }

    public Task UpdateAsync(EmployeeAllowance allowance)
    {
        _context.EmployeeAllowances.Update(allowance);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
