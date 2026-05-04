using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _context;

    public PositionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Position>> GetAllAsync()
    {
        return await _context.Positions.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<List<Position>> GetActiveAsync()
    {
        return await _context.Positions
            .Where(p => p.Status == PositionStatus.Active.ToValue())
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Position?> GetByIdAsync(Guid id)
    {
        return await _context.Positions.FindAsync(id);
    }

    public async Task<Position?> GetByCodeAsync(string code)
    {
        return await _context.Positions.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task AddAsync(Position position)
    {
        await _context.Positions.AddAsync(position);
    }

    public Task UpdateAsync(Position position)
    {
        _context.Positions.Update(position);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
