using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class DependentFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn nhân viên")]
    [Display(Name = "Nhân viên")]
    public Guid EmpId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên người phụ thuộc")]
    [MaxLength(100)]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Ngày sinh")]
    public DateOnly? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn mối quan hệ")]
    [Display(Name = "Mối quan hệ")]
    public string Relationship { get; set; } = "CHILD";

    [MaxLength(20)]
    [Display(Name = "Số CMND/CCCD")]
    public string? NationalId { get; set; }

    public List<SelectListItem> EmployeeOptions { get; set; } = new();

    public List<SelectListItem> RelationshipOptions { get; set; } = new()
    {
        new SelectListItem("Vợ/Chồng", "SPOUSE"),
        new SelectListItem("Con", "CHILD"),
        new SelectListItem("Bố/Mẹ", "PARENT")
    };
}

public class DependentIndexViewModel
{
    public List<EmployeeDependent> Dependents { get; set; } = new();
    public Guid? FilterEmpId { get; set; }
    public List<SelectListItem> EmployeeOptions { get; set; } = new();
}
