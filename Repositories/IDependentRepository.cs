using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IDependentRepository
{
    Task<List<EmployeeDependent>> GetAllAsync();
    Task<List<EmployeeDependent>> GetByEmployeeIdAsync(Guid empId);
    Task<EmployeeDependent?> GetByIdAsync(Guid id);
    Task AddAsync(EmployeeDependent dependent);
    Task UpdateAsync(EmployeeDependent dependent);
    Task SaveChangesAsync();
}
