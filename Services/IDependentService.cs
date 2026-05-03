using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IDependentService
{
    Task<List<EmployeeDependent>> GetAllAsync();
    Task<List<EmployeeDependent>> GetByEmployeeIdAsync(Guid empId);
    Task<EmployeeDependent?> GetByIdAsync(Guid id);
    Task<string?> CreateAsync(DependentFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, DependentFormViewModel vm, string updatedBy);
    Task<string?> DeactivateAsync(Guid id, string updatedBy);
}
