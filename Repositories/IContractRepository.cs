using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IContractRepository
{
    Task<List<Contract>> GetAllAsync();
    Task<List<Contract>> GetByEmployeeIdAsync(Guid empId);
    Task<Contract?> GetByIdAsync(Guid id);
    Task<Contract?> GetActiveByEmployeeIdAsync(Guid empId);
    Task<bool> HasContractOnDateAsync(Guid empId, DateOnly startDate, Guid? excludeId = null);
    Task<int> CountByYearAsync(int year);
    Task AddAsync(Contract contract);
    Task UpdateAsync(Contract contract);
    Task SaveChangesAsync();
}
