using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IPositionService
{
    Task<List<Position>> GetAllAsync();
    Task<List<Position>> GetActiveAsync();
    Task<Position?> GetByIdAsync(Guid id);
    Task<string?> CreateAsync(PositionFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, PositionFormViewModel vm, string updatedBy);
    Task<string?> DeactivateAsync(Guid id, string updatedBy);
    Task<string?> ToggleStatusAsync(Guid id, string updatedBy);
}
