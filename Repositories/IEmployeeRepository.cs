using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();
    Task<List<Employee>> SearchAsync(string? keyword, Guid? deptId, string? status);
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee?> GetByIdCardAsync(string idCard);
    Task<int> CountByDateAsync(DateOnly date);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task SaveChangesAsync();
}
