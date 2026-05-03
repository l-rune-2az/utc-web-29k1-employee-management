using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IAllowanceConfigService
{
    Task<List<AllowanceConfig>> GetAllAsync();
    Task<List<AllowanceConfig>> GetActiveAsync();
    Task<AllowanceConfig?> GetByIdAsync(Guid id);
    Task<string?> CreateAsync(AllowanceConfigFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, AllowanceConfigFormViewModel vm, string updatedBy);
    Task<string?> DeactivateAsync(Guid id, string updatedBy);
    Task<string?> ToggleStatusAsync(Guid id, string updatedBy);
}
