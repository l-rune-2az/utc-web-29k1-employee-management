using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class PositionFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã chức vụ")]
    [MaxLength(20, ErrorMessage = "Mã chức vụ tối đa 20 ký tự")]
    [Display(Name = "Mã chức vụ")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên chức vụ")]
    [MaxLength(100, ErrorMessage = "Tên chức vụ tối đa 100 ký tự")]
    [Display(Name = "Tên chức vụ")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn cấp bậc")]
    [Display(Name = "Cấp bậc")]
    public string Level { get; set; } = "JUNIOR";

    // Danh sách cấp bậc để hiển thị trong dropdown
    public List<SelectListItem> LevelOptions { get; set; } = new()
    {
        new SelectListItem("Junior (Mới vào)", "JUNIOR"),
        new SelectListItem("Middle (Có kinh nghiệm)", "MIDDLE"),
        new SelectListItem("Senior (Cấp cao)", "SENIOR")
    };
}

public class PositionDetailViewModel
{
    public Position Position { get; set; } = null!;
    public List<Employee> Employees { get; set; } = new();
}

public class PositionIndexViewModel
{
    public List<Position> Positions { get; set; } = new();
    public string? SearchKeyword { get; set; }
    public int Page      { get; set; } = 1;
    public int PageSize  { get; set; } = 10;
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}