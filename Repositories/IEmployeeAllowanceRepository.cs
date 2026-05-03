using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IEmployeeAllowanceRepository
{
    Task<List<EmployeeAllowance>> GetAllAsync();
    Task<List<EmployeeAllowance>> GetByEmployeeIdAsync(Guid empId);
    Task<List<EmployeeAllowance>> GetByContractIdAsync(Guid contractId);
    Task<EmployeeAllowance?> GetByIdAsync(Guid id);
    Task AddAsync(EmployeeAllowance allowance);
    Task UpdateAsync(EmployeeAllowance allowance);
    Task SaveChangesAsync();
}
