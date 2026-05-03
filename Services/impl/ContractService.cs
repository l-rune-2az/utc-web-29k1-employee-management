using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _repo;

    public ContractService(IContractRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Contract>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task<List<Contract>> GetByEmployeeIdAsync(Guid empId) => await _repo.GetByEmployeeIdAsync(empId);
    public async Task<Contract?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(ContractFormViewModel vm, string createdBy)
    {
        bool hasDuplicate = await _repo.HasContractOnDateAsync(vm.EmpId, vm.StartDate);
        if (hasDuplicate)
            return "Nhân viên đã có hợp đồng bắt đầu vào ngày này";

        if (vm.EndDate.HasValue && vm.EndDate.Value < vm.StartDate)
            return "Ngày kết thúc phải sau ngày bắt đầu";

        Contract? activeContract = await _repo.GetActiveByEmployeeIdAsync(vm.EmpId);
        if (activeContract != null)
        {
            activeContract.Status = ContractStatus.Expired.ToValue();
            activeContract.UpdatedBy = createdBy;
            activeContract.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(activeContract);
        }

        int year = vm.StartDate.Year;
        int count = await _repo.CountByYearAsync(year);
        string contractNumber = $"HĐ-{year}-{(count + 1):D3}";

        Contract contract = new Contract
        {
            EmpId = vm.EmpId,
            ContractNumber = contractNumber,
            ContractType = vm.ContractType,
            StartDate = vm.StartDate,
            EndDate = vm.EndDate,
            BaseSalary = vm.BaseSalary,
            OfferSalary = vm.OfferSalary,
            SalaryType = vm.SalaryType,
            Notes = vm.Notes?.Trim(),
            Status = ContractStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(contract);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, ContractFormViewModel vm, string updatedBy)
    {
        Contract? contract = await _repo.GetByIdAsync(id);
        if (contract == null) return "Không tìm thấy hợp đồng";

        if (vm.EndDate.HasValue && vm.EndDate.Value < vm.StartDate)
            return "Ngày kết thúc phải sau ngày bắt đầu";

        bool hasDuplicate = await _repo.HasContractOnDateAsync(vm.EmpId, vm.StartDate, id);
        if (hasDuplicate)
            return "Nhân viên đã có hợp đồng bắt đầu vào ngày này";

        contract.ContractType = vm.ContractType;
        contract.StartDate = vm.StartDate;
        contract.EndDate = vm.EndDate;
        contract.BaseSalary = vm.BaseSalary;
        contract.OfferSalary = vm.OfferSalary;
        contract.SalaryType = vm.SalaryType;
        contract.Notes = vm.Notes?.Trim();
        contract.UpdatedBy = updatedBy;
        contract.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(contract);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> TerminateAsync(Guid id, string updatedBy)
    {
        Contract? contract = await _repo.GetByIdAsync(id);
        if (contract == null) return "Không tìm thấy hợp đồng";

        contract.Status = ContractStatus.Terminated.ToValue();
        contract.UpdatedBy = updatedBy;
        contract.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(contract);
        await _repo.SaveChangesAsync();
        return null;
    }
}
