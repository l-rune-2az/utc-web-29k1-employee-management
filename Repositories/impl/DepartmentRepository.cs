using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Repositories;

// Lớp thực thi — chứa code truy vấn database thực tế
public class DepartmentRepository : IDepartmentRepository
{
    // _context là cầu nối đến database
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetAllAsync()
    {
        // Include(d => d.Parent) — nạp thêm thông tin phòng ban cha cùng 1 lần truy vấn
        return await _context.Departments
            .Include(d => d.Parent)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<List<Department>> GetActiveAsync()
    {
        return await _context.Departments
            .Where(d => d.Status == DepartmentStatus.Active.ToValue())
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(Guid id)
    {
        return await _context.Departments
            .Include(d => d.Parent)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Department?> GetByCodeAsync(string code)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(d => d.Code == code);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
    }

    public Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
