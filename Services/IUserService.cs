using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Services;

public interface IUserService
{
    Task<Users?> AuthenticateAsync(string username, string password);
    Task<string?> ChangePasswordAsync(string username, string currentPassword, string newPassword);
}
