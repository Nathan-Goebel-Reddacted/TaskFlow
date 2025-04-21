using TaskFlow.Storage.EFcore.Models;

namespace TaskFlow.Storage.EFcore.Repositories;

public interface IProjectsRepository
{
    Task<Project?> GetByIdAsync(int id);
    Task<List<Project>> GetAllAsync();
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Project project);
    Task SaveChangesAsync();
}
