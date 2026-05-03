using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .OrderBy(e => e.FullName)
            .ToListAsync();
    }

    public async Task<List<Employee>> SearchAsync(string? keyword, Guid? deptId, string? status)
    {
        // IQueryable cho phép xây dựng câu truy vấn từng bước, chỉ chạy khi gọi ToListAsync()
        IQueryable<Employee> query = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            // Tìm kiếm theo tên hoặc mã nhân viên
            keyword = keyword.Trim().ToLower();
            query = query.Where(e =>
                e.FullName.ToLower().Contains(keyword) ||
                e.Code.ToLower().Contains(keyword) ||
                e.Email.ToLower().Contains(keyword));
        }

        if (deptId.HasValue)
            query = query.Where(e => e.DeptId == deptId.Value);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(e => e.Status == status);

        return await query.OrderBy(e => e.FullName).ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<Employee?> GetByIdCardAsync(string idCard)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.IdCard == idCard);
    }

    // Đếm số nhân viên được tạo trong ngày — dùng để sinh mã NV
    public async Task<int> CountByDateAsync(DateOnly date)
    {
        DateTime startOfDay = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        DateTime endOfDay = date.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
        return await _context.Employees
            .Where(e => e.CreatedAt >= startOfDay && e.CreatedAt <= endOfDay)
            .CountAsync();
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
