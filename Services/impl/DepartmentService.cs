using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;

    public DepartmentService(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Department>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task<List<Department>> GetActiveAsync() => await _repo.GetActiveAsync();
    public async Task<Department?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(DepartmentFormViewModel vm, string createdBy)
    {
        
        Department? existing = await _repo.GetByCodeAsync(vm.Code.ToUpper());
        if (existing != null)
            return $"Mã phòng ban '{vm.Code}' đã tồn tại";

        Department dept = new Department
        {
            Code = vm.Code.ToUpper().Trim(),
            Name = vm.Name.Trim(),
            Description = vm.Description?.Trim(),
            ParentId = vm.ParentId,
            Status = DepartmentStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(dept);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, DepartmentFormViewModel vm, string updatedBy)
    {
        Department? dept = await _repo.GetByIdAsync(id);
        if (dept == null) return "Không tìm thấy phòng ban";

        
        Department? existing = await _repo.GetByCodeAsync(vm.Code.ToUpper());
        if (existing != null && existing.Id != id)
            return $"Mã phòng ban '{vm.Code}' đã tồn tại";

        
        if (vm.ParentId == id)
            return "Phòng ban không thể là phòng ban cha của chính nó";

        dept.Code = vm.Code.ToUpper().Trim();
        dept.Name = vm.Name.Trim();
        dept.Description = vm.Description?.Trim();
        dept.ParentId = vm.ParentId;
        dept.UpdatedBy = updatedBy;
        dept.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(dept);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeactivateAsync(Guid id, string updatedBy)
    {
        Department? dept = await _repo.GetByIdAsync(id);
        if (dept == null) return "Không tìm thấy phòng ban";

        dept.Status = DepartmentStatus.Inactive.ToValue();
        dept.UpdatedBy = updatedBy;
        dept.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(dept);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> ToggleStatusAsync(Guid id, string updatedBy)
    {
        Department? dept = await _repo.GetByIdAsync(id);
        if (dept == null) return "Không tìm thấy phòng ban";

        dept.Status = dept.Status == DepartmentStatus.Active.ToValue()
            ? DepartmentStatus.Inactive.ToValue()
            : DepartmentStatus.Active.ToValue();
        dept.UpdatedBy = updatedBy;
        dept.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(dept);
        await _repo.SaveChangesAsync();
        return null;
    }
}
