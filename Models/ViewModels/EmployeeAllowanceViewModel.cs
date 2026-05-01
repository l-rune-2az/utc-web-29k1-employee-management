using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class EmployeeAllowanceFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn nhân viên")]
    [Display(Name = "Nhân viên")]
    public Guid EmpId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn hợp đồng")]
    [Display(Name = "Hợp đồng")]
    public Guid ContractId { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn loại phụ cấp")]
    [Display(Name = "Loại phụ cấp")]
    public Guid AllowanceId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số tiền")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
    [Display(Name = "Số tiền (VNĐ)")]
    public decimal Amount { get; set; }

    [Display(Name = "Ngày áp dụng")]
    public DateOnly? EffectiveDate { get; set; }

    public List<SelectListItem> EmployeeOptions { get; set; } = new();
    public List<SelectListItem> ContractOptions { get; set; } = new();
    public List<SelectListItem> AllowanceOptions { get; set; } = new();
}

public class EmployeeAllowanceIndexViewModel
{
    public List<EmployeeAllowance> EmployeeAllowances { get; set; } = new();
    public Guid? FilterEmpId { get; set; }
    public List<SelectListItem> EmployeeOptions { get; set; } = new();
    public List<SelectListItem> ContractOptions  { get; set; } = new();
    public List<SelectListItem> AllowanceOptions { get; set; } = new();
}
