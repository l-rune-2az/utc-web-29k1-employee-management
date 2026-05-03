using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IAllowanceConfigRepository
{
    Task<List<AllowanceConfig>> GetAllAsync();
    Task<List<AllowanceConfig>> GetActiveAsync();
    Task<AllowanceConfig?> GetByIdAsync(Guid id);
    Task<AllowanceConfig?> GetByCodeAsync(string code);
    Task AddAsync(AllowanceConfig config);
    Task UpdateAsync(AllowanceConfig config);
    Task SaveChangesAsync();
}
