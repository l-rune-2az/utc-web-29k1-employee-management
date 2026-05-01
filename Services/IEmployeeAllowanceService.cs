using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IEmployeeAllowanceService
{
    Task<List<EmployeeAllowance>> GetAllAsync();
    Task<List<EmployeeAllowance>> GetByEmployeeIdAsync(Guid empId);
    Task<List<EmployeeAllowance>> GetByContractIdAsync(Guid contractId);
    Task<EmployeeAllowance?> GetByIdAsync(Guid id);
    Task<string?> CreateAsync(EmployeeAllowanceFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, EmployeeAllowanceFormViewModel vm, string updatedBy);
    Task<string?> DeactivateAsync(Guid id, string updatedBy);
}
