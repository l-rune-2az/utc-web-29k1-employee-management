using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class AllowanceConfigService : IAllowanceConfigService
{
    private readonly IAllowanceConfigRepository _repo;

    public AllowanceConfigService(IAllowanceConfigRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AllowanceConfig>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task<List<AllowanceConfig>> GetActiveAsync() => await _repo.GetActiveAsync();
    public async Task<AllowanceConfig?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(AllowanceConfigFormViewModel vm, string createdBy)
    {
        AllowanceConfig? existing = await _repo.GetByCodeAsync(vm.Code.ToUpper());
        if (existing != null)
            return $"Mã phụ cấp '{vm.Code}' đã tồn tại";

        AllowanceConfig config = new AllowanceConfig
        {
            Code = vm.Code.ToUpper().Trim(),
            Name = vm.Name.Trim(),
            Description = vm.Description?.Trim(),
            DefaultAmount = vm.DefaultAmount,
            Status = AllowanceStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(config);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, AllowanceConfigFormViewModel vm, string updatedBy)
    {
        AllowanceConfig? config = await _repo.GetByIdAsync(id);
        if (config == null) return "Không tìm thấy loại phụ cấp";

        AllowanceConfig? existing = await _repo.GetByCodeAsync(vm.Code.ToUpper());
        if (existing != null && existing.Id != id)
            return $"Mã phụ cấp '{vm.Code}' đã tồn tại";

        config.Code = vm.Code.ToUpper().Trim();
        config.Name = vm.Name.Trim();
        config.Description = vm.Description?.Trim();
        config.DefaultAmount = vm.DefaultAmount;
        config.UpdatedBy = updatedBy;
        config.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(config);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeactivateAsync(Guid id, string updatedBy)
    {
        AllowanceConfig? config = await _repo.GetByIdAsync(id);
        if (config == null) return "Không tìm thấy loại phụ cấp";

        config.Status = AllowanceStatus.Inactive.ToValue();
        config.UpdatedBy = updatedBy;
        config.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(config);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> ToggleStatusAsync(Guid id, string updatedBy)
    {
        AllowanceConfig? config = await _repo.GetByIdAsync(id);
        if (config == null) return "Không tìm thấy loại phụ cấp";

        config.Status = config.Status == AllowanceStatus.Active.ToValue()
            ? AllowanceStatus.Inactive.ToValue()
            : AllowanceStatus.Active.ToValue();
        config.UpdatedBy = updatedBy;
        config.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(config);
        await _repo.SaveChangesAsync();
        return null;
    }
}
