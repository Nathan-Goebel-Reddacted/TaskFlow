using Microsoft.EntityFrameworkCore;
using TaskFlow.Storage.EFcore.Context;
using TaskFlow.Storage.EFcore.Models;

namespace TaskFlow.Storage.EFcore.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly TaskFlowDbContext _context;

    public ProjectsRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(int id) =>
        await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Project>> GetAllAsync() =>
        await _context.Projects.Include(p => p.Tasks).ToListAsync();

    public async Task AddAsync(Project project) =>
        await _context.Projects.AddAsync(project);

    public async Task UpdateAsync(Project project) =>
        _context.Projects.Update(project);

    public async Task DeleteAsync(Project project) =>
        _context.Projects.Remove(project);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}

