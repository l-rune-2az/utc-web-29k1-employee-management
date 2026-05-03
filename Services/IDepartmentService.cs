using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services;

public interface IDepartmentService
{
    Task<List<Department>> GetAllAsync();
    Task<List<Department>> GetActiveAsync();
    Task<Department?> GetByIdAsync(Guid id);
    // Trả về null nếu thành công, trả về thông báo lỗi nếu thất bại
    Task<string?> CreateAsync(DepartmentFormViewModel vm, string createdBy);
    Task<string?> UpdateAsync(Guid id, DepartmentFormViewModel vm, string updatedBy);
    Task<string?> DeactivateAsync(Guid id, string updatedBy);
    Task<string?> ToggleStatusAsync(Guid id, string updatedBy);
}
