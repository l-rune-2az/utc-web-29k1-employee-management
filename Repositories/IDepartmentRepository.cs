using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

// Interface định nghĩa các thao tác với bảng department
// Tách biệt "hợp đồng giao tiếp" khỏi "cách thực hiện" — giúp dễ thay đổi sau này
public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync();
    Task<List<Department>> GetActiveAsync();
    Task<Department?> GetByIdAsync(Guid id);
    Task<Department?> GetByCodeAsync(string code);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task SaveChangesAsync();
}