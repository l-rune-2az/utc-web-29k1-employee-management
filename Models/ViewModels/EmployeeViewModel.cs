using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class EmployeeFormViewModel
{
    public Guid? Id { get; set; }

    // Mã nhân viên — tự động sinh, chỉ hiển thị khi Edit
    [Display(Name = "Mã nhân viên")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
    [MaxLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn giới tính")]
    [Display(Name = "Giới tính")]
    public string Gender { get; set; } = "MALE";

    [Display(Name = "Ngày sinh")]
    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(20)]
    [Display(Name = "Số CMND/CCCD")]
    public string? IdCard { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [MaxLength(150)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    [Display(Name = "Số điện thoại")]
    public string? Phone { get; set; }

    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Display(Name = "Ngày vào làm")]
    public DateOnly? HireDate { get; set; }

    [Display(Name = "Phòng ban")]
    public Guid? DeptId { get; set; }

    [Display(Name = "Chức vụ")]
    public Guid? PositionId { get; set; }

    // Dropdown options — được điền từ Controller
    public List<SelectListItem> DepartmentOptions { get; set; } = new();
    public List<SelectListItem> PositionOptions { get; set; } = new();

    public List<SelectListItem> GenderOptions { get; set; } = new()
    {
        new SelectListItem("Nam", "MALE"),
        new SelectListItem("Nữ", "FEMALE"),
        new SelectListItem("Khác", "OTHER")
    };
}

public class EmployeeDetailViewModel
{
    public Employee Employee { get; set; } = null!;
    public List<Contract> Contracts { get; set; } = new();
    public List<EmployeeDependent> Dependents { get; set; } = new();
    public List<EmployeeAllowance> Allowances { get; set; } = new();
}

public class EmployeeIndexViewModel
{
    public List<Employee> Employees { get; set; } = new();
    public string? SearchKeyword { get; set; }
    public Guid? FilterDeptId { get; set; }
    public string? FilterStatus { get; set; }
    public List<SelectListItem> DepartmentOptions { get; set; } = new();
    public List<SelectListItem> PositionOptions   { get; set; } = new();
    public int Page      { get; set; } = 1;
    public int PageSize  { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
