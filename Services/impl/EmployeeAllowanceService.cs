using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class EmployeeAllowanceService : IEmployeeAllowanceService
{
    private readonly IEmployeeAllowanceRepository _repo;

    public EmployeeAllowanceService(IEmployeeAllowanceRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EmployeeAllowance>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task<List<EmployeeAllowance>> GetByEmployeeIdAsync(Guid empId) => await _repo.GetByEmployeeIdAsync(empId);
    public async Task<List<EmployeeAllowance>> GetByContractIdAsync(Guid contractId) => await _repo.GetByContractIdAsync(contractId);
    public async Task<EmployeeAllowance?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(EmployeeAllowanceFormViewModel vm, string createdBy)
    {
        EmployeeAllowance allowance = new EmployeeAllowance
        {
            EmpId = vm.EmpId,
            ContractId = vm.ContractId,
            AllowanceId = vm.AllowanceId,
            Amount = vm.Amount,
            EffectiveDate = vm.EffectiveDate,
            Status = AllowanceStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(allowance);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, EmployeeAllowanceFormViewModel vm, string updatedBy)
    {
        EmployeeAllowance? allowance = await _repo.GetByIdAsync(id);
        if (allowance == null) return "Không tìm thấy phụ cấp";

        allowance.AllowanceId = vm.AllowanceId;
        allowance.Amount = vm.Amount;
        allowance.EffectiveDate = vm.EffectiveDate;
        allowance.UpdatedBy = updatedBy;
        allowance.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(allowance);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeactivateAsync(Guid id, string updatedBy)
    {
        EmployeeAllowance? allowance = await _repo.GetByIdAsync(id);
        if (allowance == null) return "Không tìm thấy phụ cấp";

        allowance.Status = AllowanceStatus.Inactive.ToValue();
        allowance.UpdatedBy = updatedBy;
        allowance.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(allowance);
        await _repo.SaveChangesAsync();
        return null;
    }
}
