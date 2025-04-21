using Microsoft.EntityFrameworkCore;
using TaskFlow.Storage.EFcore.Context;
using TaskFlow.Storage.EFcore.Models;

namespace TaskFlow.Storage.EFcore.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly TaskFlowDbContext _context;

    public TasksRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _context.Tasks.FindAsync(id);

    public async Task<List<TaskItem>> GetAllAsync() =>
        await _context.Tasks.ToListAsync();

    public async Task AddAsync(TaskItem task) =>
        await _context.Tasks.AddAsync(task);

    public async Task UpdateAsync(TaskItem task) =>
        _context.Tasks.Update(task);

    public async Task DeleteAsync(TaskItem task) =>
        _context.Tasks.Remove(task);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}

