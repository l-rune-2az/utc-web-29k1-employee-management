using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IPositionRepository
{
    Task<List<Position>> GetAllAsync();
    Task<List<Position>> GetActiveAsync();
    Task<Position?> GetByIdAsync(Guid id);
    Task<Position?> GetByCodeAsync(string code);
    Task AddAsync(Position position);
    Task UpdateAsync(Position position);
    Task SaveChangesAsync();
}