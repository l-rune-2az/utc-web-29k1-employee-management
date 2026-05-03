using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Services;

// Service xử lý logic liên quan đến tài khoản người dùng
public interface IUserService
{
    // Xác thực đăng nhập — trả về null nếu thất bại
    Task<Users?> AuthenticateAsync(string username, string password);
}
