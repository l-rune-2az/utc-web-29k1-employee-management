using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IContractService
{
    Task<List<Contract>> GetAllAsync();
    Task<List<Contract>> GetByEmployeeIdAsync(Guid empId);
    Task<Contract?> GetByIdAsync(Guid id);
    Task<string?> CreateAsync(ContractFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, ContractFormViewModel vm, string updatedBy);
    Task<string?> TerminateAsync(Guid id, string updatedBy);
}
