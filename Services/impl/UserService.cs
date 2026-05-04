using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<Users?> AuthenticateAsync(string username, string password)
    {
        Users? user = await _repo.GetByUsernameAsync(username);

        // Tài khoản không tồn tại
        if (user == null) return null;

        // Tài khoản đang bị khóa — kiểm tra thời gian hết khóa
        if (user.Status == UserStatus.Locked.ToValue())
        {
            if (user.LockedUntil.HasValue && DateTime.UtcNow < user.LockedUntil.Value)
                return null; // Vẫn còn trong thời gian bị khóa

            // Hết thời gian khóa → tự động mở khóa
            user.Status = UserStatus.Active.ToValue();
            user.FailedAttempts = 0;
            user.LockedUntil = null;
            await _repo.UpdateAsync(user);
            await _repo.SaveChangesAsync();
        }

        if (user.Status != UserStatus.Active.ToValue()) return null;

        // BCrypt.Verify — so sánh mật khẩu nhập vào với hash đã lưu
        // Không bao giờ lưu mật khẩu gốc, chỉ lưu hash
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            // Sai mật khẩu → tăng số lần thất bại
            user.FailedAttempts++;
            if (user.FailedAttempts >= 5)
            {
                // Sau 5 lần sai → khóa tài khoản 15 phút
                user.Status = UserStatus.Locked.ToValue();
                user.LockedUntil = DateTime.UtcNow.AddMinutes(15);
            }
            await _repo.UpdateAsync(user);
            await _repo.SaveChangesAsync();
            return null;
        }

        // Đăng nhập thành công → reset số lần thất bại
        user.FailedAttempts = 0;
        user.LockedUntil = null;
        await _repo.UpdateAsync(user);
        await _repo.SaveChangesAsync();

        return user;
    }
}
