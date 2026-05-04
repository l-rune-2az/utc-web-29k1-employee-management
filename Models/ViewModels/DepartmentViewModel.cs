using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class DepartmentFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã phòng ban")]
    [MaxLength(20, ErrorMessage = "Mã phòng ban tối đa 20 ký tự")]
    [Display(Name = "Mã phòng ban")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên phòng ban")]
    [MaxLength(100, ErrorMessage = "Tên phòng ban tối đa 100 ký tự")]
    [Display(Name = "Tên phòng ban")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Phòng ban cha")]
    public Guid? ParentId { get; set; }

    
    public List<SelectListItem> ParentOptions { get; set; } = new();
}

public class DepartmentDetailViewModel
{
    public Department Department { get; set; } = null!;
    public List<Department> SubDepartments { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
}

public class DepartmentIndexViewModel
{
    public List<Department> Departments { get; set; } = new();
    public string? SearchKeyword { get; set; }
    public int Page      { get; set; } = 1;
    public int PageSize  { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public List<SelectListItem> ParentOptions { get; set; } = new();
}
