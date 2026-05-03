using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class ContractFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn nhân viên")]
    [Display(Name = "Nhân viên")]
    public Guid EmpId { get; set; }

    [MaxLength(50)]
    [Display(Name = "Số hợp đồng")]
    public string ContractNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn loại hợp đồng")]
    [Display(Name = "Loại hợp đồng")]
    public string ContractType { get; set; } = "OFFICIAL";

    [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu")]
    [Display(Name = "Ngày bắt đầu")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "Ngày kết thúc (để trống = không thời hạn)")]
    public DateOnly? EndDate { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập lương cơ bản")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Lương cơ bản phải lớn hơn 0")]
    [Display(Name = "Lương cơ bản")]
    public decimal BaseSalary { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập lương thực nhận")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Lương thực nhận phải lớn hơn 0")]
    [Display(Name = "Lương thực nhận")]
    public decimal OfferSalary { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn loại lương")]
    [Display(Name = "Loại lương")]
    public string SalaryType { get; set; } = "GROSS";

    [Display(Name = "Ghi chú")]
    public string? Notes { get; set; }

    public List<SelectListItem> EmployeeOptions { get; set; } = new();

    public List<SelectListItem> ContractTypeOptions { get; set; } = new()
    {
        new SelectListItem("Thử việc", "PROBATION"),
        new SelectListItem("Chính thức", "OFFICIAL"),
        new SelectListItem("Thời vụ", "SEASONAL")
    };

    public List<SelectListItem> SalaryTypeOptions { get; set; } = new()
    {
        new SelectListItem("Gross (trước thuế)", "GROSS"),
        new SelectListItem("Net (sau thuế)", "NET")
    };
}

public class ContractDetailViewModel
{
    public Contract Contract { get; set; } = null!;
    public List<EmployeeAllowance> Allowances { get; set; } = new();
}

public class ContractIndexViewModel
{
    public List<Contract> Contracts { get; set; } = new();
    public string? SearchKeyword { get; set; }
    public string? FilterStatus { get; set; }
    public int Page      { get; set; } = 1;
    public int PageSize  { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public List<SelectListItem> EmployeeOptions    { get; set; } = new();
}
