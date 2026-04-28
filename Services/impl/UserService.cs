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

        if (user == null) return null;

        if (user.Status == UserStatus.Locked.ToValue())
        {
            if (user.LockedUntil.HasValue && DateTime.UtcNow < user.LockedUntil.Value)
                return null;

            user.Status = UserStatus.Active.ToValue();
            user.FailedAttempts = 0;
            user.LockedUntil = null;
            await _repo.UpdateAsync(user);
            await _repo.SaveChangesAsync();
        }

        if (user.Status != UserStatus.Active.ToValue()) return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            user.FailedAttempts++;
            if (user.FailedAttempts >= 5)
            {
                user.Status = UserStatus.Locked.ToValue();
                user.LockedUntil = DateTime.UtcNow.AddMinutes(15);
            }
            await _repo.UpdateAsync(user);
            await _repo.SaveChangesAsync();
            return null;
        }

        user.FailedAttempts = 0;
        user.LockedUntil = null;
        await _repo.UpdateAsync(user);
        await _repo.SaveChangesAsync();

        return user;
    }
}
