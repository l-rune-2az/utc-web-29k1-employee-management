using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Employee>> SearchAsync(string? keyword, Guid? deptId, string? status)
        => await _repo.SearchAsync(keyword, deptId, status);

    public async Task<Employee?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

    public async Task<string?> CreateAsync(EmployeeFormViewModel vm, string createdBy)
    {
        // Kiểm tra email trùng
        if (!string.IsNullOrWhiteSpace(vm.Email))
        {
            Employee? emailExists = await _repo.GetByEmailAsync(vm.Email.Trim().ToLower());
            if (emailExists != null)
                return $"Email '{vm.Email}' đã được sử dụng";
        }

        // Kiểm tra CMND/CCCD trùng
        if (!string.IsNullOrWhiteSpace(vm.IdCard))
        {
            Employee? idCardExists = await _repo.GetByIdCardAsync(vm.IdCard.Trim());
            if (idCardExists != null)
                return $"Số CMND/CCCD '{vm.IdCard}' đã được sử dụng";
        }

        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
        int countToday = await _repo.CountByDateAsync(today);

        // Sinh mã nhân viên: NV + YYMMDD + số thứ tự 3 chữ số
        // Ví dụ: NV26042800001 (ngày 28/04/2026, nhân viên thứ 1)
        string code = $"NV{today:yyMMdd}{(countToday + 1):D3}";

        Employee employee = new Employee
        {
            Code = code,
            FullName = vm.FullName.Trim(),
            Gender = vm.Gender,
            DateOfBirth = vm.DateOfBirth,
            IdCard = vm.IdCard?.Trim(),
            Email = vm.Email.Trim().ToLower(),
            Phone = vm.Phone?.Trim(),
            Address = vm.Address?.Trim(),
            HireDate = vm.HireDate,
            DeptId = vm.DeptId,
            PositionId = vm.PositionId,
            Status = EmployeeStatus.Active.ToValue(),
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = createdBy,
            UpdatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(employee);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> UpdateAsync(Guid id, EmployeeFormViewModel vm, string updatedBy)
    {
        Employee? employee = await _repo.GetByIdAsync(id);
        if (employee == null) return "Không tìm thấy nhân viên";

        // Kiểm tra email mới có trùng với nhân viên khác không
        Employee? emailExists = await _repo.GetByEmailAsync(vm.Email.Trim().ToLower());
        if (emailExists != null && emailExists.Id != id)
            return $"Email '{vm.Email}' đã được sử dụng";

        if (!string.IsNullOrWhiteSpace(vm.IdCard))
        {
            Employee? idCardExists = await _repo.GetByIdCardAsync(vm.IdCard.Trim());
            if (idCardExists != null && idCardExists.Id != id)
                return $"Số CMND/CCCD '{vm.IdCard}' đã được sử dụng";
        }

        employee.FullName = vm.FullName.Trim();
        employee.Gender = vm.Gender;
        employee.DateOfBirth = vm.DateOfBirth;
        employee.IdCard = vm.IdCard?.Trim();
        employee.Email = vm.Email.Trim().ToLower();
        employee.Phone = vm.Phone?.Trim();
        employee.Address = vm.Address?.Trim();
        employee.HireDate = vm.HireDate;
        employee.DeptId = vm.DeptId;
        employee.PositionId = vm.PositionId;
        employee.UpdatedBy = updatedBy;
        employee.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(employee);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> DeactivateAsync(Guid id, string updatedBy)
    {
        Employee? employee = await _repo.GetByIdAsync(id);
        if (employee == null) return "Không tìm thấy nhân viên";

        employee.Status = EmployeeStatus.Inactive.ToValue();
        employee.UpdatedBy = updatedBy;
        employee.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(employee);
        await _repo.SaveChangesAsync();
        return null;
    }

    public async Task<string?> ToggleStatusAsync(Guid id, string updatedBy)
    {
        Employee? employee = await _repo.GetByIdAsync(id);
        if (employee == null) return "Không tìm thấy nhân viên";

        employee.Status = employee.Status == EmployeeStatus.Active.ToValue()
            ? EmployeeStatus.Inactive.ToValue()
            : EmployeeStatus.Active.ToValue();
        employee.UpdatedBy = updatedBy;
        employee.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(employee);
        await _repo.SaveChangesAsync();
        return null;
    }
}
