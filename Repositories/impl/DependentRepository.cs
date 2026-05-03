using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public class DependentRepository : IDependentRepository
{
    private readonly ApplicationDbContext _context;

    public DependentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDependent>> GetAllAsync()
    {
        return await _context.EmployeeDependents
            .Include(d => d.Employee)
            .OrderBy(d => d.FullName)
            .ToListAsync();
    }

    public async Task<List<EmployeeDependent>> GetByEmployeeIdAsync(Guid empId)
    {
        return await _context.EmployeeDependents
            .Where(d => d.EmpId == empId)
            .OrderBy(d => d.FullName)
            .ToListAsync();
    }

    public async Task<EmployeeDependent?> GetByIdAsync(Guid id)
    {
        return await _context.EmployeeDependents
            .Include(d => d.Employee)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(EmployeeDependent dependent)
    {
        await _context.EmployeeDependents.AddAsync(dependent);
    }

    public Task UpdateAsync(EmployeeDependent dependent)
    {
        _context.EmployeeDependents.Update(dependent);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
