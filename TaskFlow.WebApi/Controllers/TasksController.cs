using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Storage.EFcore.Models;
using TaskFlow.Storage.EFcore.Repositories;

namespace TaskFlow.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITasksRepository _tasksRepository;

    public TasksController(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    // GET /api/tasks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _tasksRepository.GetAllAsync();
        return Ok(tasks);
    }

    // GET /api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _tasksRepository.GetByIdAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    // POST /api/tasks
    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        await _tasksRepository.AddAsync(task);
        await _tasksRepository.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // PUT /api/tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem updated)
    {
        var task = await _tasksRepository.GetByIdAsync(id);
        if (task == null)
            return NotFound();

        task.Title = updated.Title;
        task.Status = updated.Status;
        task.DueDate = updated.DueDate;
        task.Commentaires = updated.Commentaires;

        await _tasksRepository.UpdateAsync(task);
        await _tasksRepository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _tasksRepository.GetByIdAsync(id);
        if (task == null)
            return NotFound();

        await _tasksRepository.DeleteAsync(task);
        await _tasksRepository.SaveChangesAsync();

        return NoContent();
    }
}
