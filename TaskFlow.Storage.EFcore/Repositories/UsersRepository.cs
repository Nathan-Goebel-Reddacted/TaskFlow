using Microsoft.EntityFrameworkCore;
using TaskFlow.Storage.EFcore.Context;
using TaskFlow.Storage.EFcore.Models;

namespace TaskFlow.Storage.EFcore.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly TaskFlowDbContext _context;

    public UsersRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<List<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task AddAsync(User user) =>
        await _context.Users.AddAsync(user);

    public async Task DeleteAsync(User user) =>
        _context.Users.Remove(user);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
