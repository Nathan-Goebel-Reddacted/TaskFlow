using TaskFlow.Storage.EFcore.Models;

namespace TaskFlow.Storage.EFcore.Repositories;

public interface IUsersRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task DeleteAsync(User user);
    Task SaveChangesAsync();
}
