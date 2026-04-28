using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public interface IUserRepository
{
    Task<Users?> GetByUsernameAsync(string username);
    Task<Users?> GetByEmployeeIdAsync(Guid employeeId);
    Task AddAsync(Users user);
    Task UpdateAsync(Users user);
    Task SaveChangesAsync();
}
