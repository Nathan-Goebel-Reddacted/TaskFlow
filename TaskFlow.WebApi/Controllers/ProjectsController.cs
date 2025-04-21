using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Storage.EFcore.Models;
using TaskFlow.Storage.EFcore.Repositories;

namespace TaskFlow.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsRepository _projectsRepository;

    public ProjectsController(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectsRepository.GetAllAsync();
        return Ok(projects);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(Project project)
    {
        await _projectsRepository.AddAsync(project);
        await _projectsRepository.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Project updated)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        if (project == null) return NotFound();

        project.Name = updated.Name;
        project.Description = updated.Description;

        await _projectsRepository.UpdateAsync(project);
        await _projectsRepository.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        if (project == null) return NotFound();

        await _projectsRepository.DeleteAsync(project);
        await _projectsRepository.SaveChangesAsync();

        return NoContent();
    }

}
