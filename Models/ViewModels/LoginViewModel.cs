using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.ViewModels;

// ViewModel cho trang đăng nhập — chỉ chứa dữ liệu cần thiết cho form
public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [MaxLength(50)]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;
}
