using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _context;

    public ContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contract>> GetAllAsync()
    {
        return await _context.Contracts
            .Include(c => c.Employee)
            .OrderByDescending(c => c.StartDate)
            .ToListAsync();
    }

    public async Task<List<Contract>> GetByEmployeeIdAsync(Guid empId)
    {
        return await _context.Contracts
            .Where(c => c.EmpId == empId)
            .OrderByDescending(c => c.StartDate)
            .ToListAsync();
    }

    public async Task<Contract?> GetByIdAsync(Guid id)
    {
        return await _context.Contracts
            .Include(c => c.Employee)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    
    public async Task<Contract?> GetActiveByEmployeeIdAsync(Guid empId)
    {
        return await _context.Contracts
            .FirstOrDefaultAsync(c => c.EmpId == empId && c.Status == ContractStatus.Active.ToValue());
    }

    
    public async Task<bool> HasContractOnDateAsync(Guid empId, DateOnly startDate, Guid? excludeId = null)
    {
        return await _context.Contracts
            .AnyAsync(c => c.EmpId == empId && c.StartDate == startDate && c.Id != excludeId);
    }

    
    public async Task<int> CountByYearAsync(int year)
    {
        return await _context.Contracts
            .Where(c => c.StartDate.Year == year)
            .CountAsync();
    }

    public async Task AddAsync(Contract contract)
    {
        await _context.Contracts.AddAsync(contract);
    }

    public Task UpdateAsync(Contract contract)
    {
        _context.Contracts.Update(contract);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
