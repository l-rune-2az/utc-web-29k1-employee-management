using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

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
