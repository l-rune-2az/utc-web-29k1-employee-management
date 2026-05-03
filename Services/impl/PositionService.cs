using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _repo;

    public PositionService(IPositionRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Position>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task<List<Position>> GetActiveAsync() => await _repo.GetActiveAsync();
    public async Task<Position?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(PositionFormViewModel vm, string createdBy)
    {
        Position? existing = await _repo.GetByCodeAsync(vm.Code.ToUpper());
        if (existing != null)
            return $"Mã chức vụ '{vm.Code}' đã tồn tại";

        Position position = new Position
        {
            Code = vm.Code.ToUpper().Trim(),
            Name = vm.Name.Trim(),
            Description = vm.Description?.Trim(),
            Level = vm.Level,
            Status = PositionStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(position);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, PositionFormViewModel vm, string updatedBy)
    {
        Position? position = await _repo.GetByIdAsync(id);
        if (position == null) return "Không tìm thấy chức vụ";

        Position? existing = await _repo.GetByCodeAsync(vm.Code.ToUpper());
        if (existing != null && existing.Id != id)
            return $"Mã chức vụ '{vm.Code}' đã tồn tại";

        position.Code = vm.Code.ToUpper().Trim();
        position.Name = vm.Name.Trim();
        position.Description = vm.Description?.Trim();
        position.Level = vm.Level;
        position.UpdatedBy = updatedBy;
        position.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(position);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeactivateAsync(Guid id, string updatedBy)
    {
        Position? position = await _repo.GetByIdAsync(id);
        if (position == null) return "Không tìm thấy chức vụ";

        position.Status = PositionStatus.Inactive.ToValue();
        position.UpdatedBy = updatedBy;
        position.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(position);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> ToggleStatusAsync(Guid id, string updatedBy)
    {
        Position? position = await _repo.GetByIdAsync(id);
        if (position == null) return "Không tìm thấy chức vụ";

        position.Status = position.Status == PositionStatus.Active.ToValue()
            ? PositionStatus.Inactive.ToValue()
            : PositionStatus.Active.ToValue();
        position.UpdatedBy = updatedBy;
        position.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(position);
        await _repo.SaveChangesAsync();
        return null;
    }
}