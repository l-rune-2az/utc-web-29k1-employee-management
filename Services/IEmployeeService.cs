using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IEmployeeService
{
    Task<List<Employee>> SearchAsync(string? keyword, Guid? deptId, string? status);
    Task<Employee?> GetByIdAsync(Guid id);
    Task<string?> CreateAsync(EmployeeFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, EmployeeFormViewModel vm, string updatedBy);
    Task<string?> UpdateContactAsync(Guid id, string? phone, string? address, DateOnly? dateOfBirth, string updatedBy);
    Task<string?> DeactivateAsync(Guid id, string updatedBy);
    Task<string?> ToggleStatusAsync(Guid id, string updatedBy);
}
