using TaskFlow.Storage.EFcore.Models;

namespace TaskFlow.Storage.EFcore.Repositories;

public interface ITasksRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    Task<List<TaskItem>> GetAllAsync();
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
    Task SaveChangesAsync();
}

