using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class DependentService : IDependentService
{
    private readonly IDependentRepository _repo;

    public DependentService(IDependentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EmployeeDependent>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task<List<EmployeeDependent>> GetByEmployeeIdAsync(Guid empId) => await _repo.GetByEmployeeIdAsync(empId);
    public async Task<EmployeeDependent?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(DependentFormViewModel vm, string createdBy)
    {
        EmployeeDependent dependent = new EmployeeDependent
        {
            EmpId = vm.EmpId,
            FullName = vm.FullName.Trim(),
            DateOfBirth = vm.DateOfBirth,
            Relationship = vm.Relationship,
            NationalId = vm.NationalId?.Trim(),
            Status = DependentStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(dependent);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, DependentFormViewModel vm, string updatedBy)
    {
        EmployeeDependent? dependent = await _repo.GetByIdAsync(id);
        if (dependent == null) return "Không tìm thấy người phụ thuộc";

        dependent.FullName = vm.FullName.Trim();
        dependent.DateOfBirth = vm.DateOfBirth;
        dependent.Relationship = vm.Relationship;
        dependent.NationalId = vm.NationalId?.Trim();
        dependent.UpdatedBy = updatedBy;
        dependent.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(dependent);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeactivateAsync(Guid id, string updatedBy)
    {
        EmployeeDependent? dependent = await _repo.GetByIdAsync(id);
        if (dependent == null) return "Không tìm thấy người phụ thuộc";

        dependent.Status = DependentStatus.Inactive.ToValue();
        dependent.UpdatedBy = updatedBy;
        dependent.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(dependent);
        await _repo.SaveChangesAsync();
        return null;
    }
}
