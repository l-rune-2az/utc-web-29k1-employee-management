using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Users?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Users?> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
    }

    public async Task AddAsync(Users user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task UpdateAsync(Users user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
