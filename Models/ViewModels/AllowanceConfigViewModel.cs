using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class AllowanceConfigFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã phụ cấp")]
    [MaxLength(20)]
    [Display(Name = "Mã phụ cấp")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên phụ cấp")]
    [MaxLength(100)]
    [Display(Name = "Tên phụ cấp")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Mức mặc định phải >= 0")]
    [Display(Name = "Mức mặc định (VNĐ)")]
    public decimal? DefaultAmount { get; set; }
}

public class AllowanceConfigDetailViewModel
{
    public AllowanceConfig Config { get; set; } = null!;
    public List<EmployeeAllowance> Usages { get; set; } = new();
}

public class AllowanceConfigIndexViewModel
{
    public List<AllowanceConfig> AllowanceConfigs { get; set; } = new();
    public string? SearchKeyword { get; set; }
    public int Page      { get; set; } = 1;
    public int PageSize  { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
