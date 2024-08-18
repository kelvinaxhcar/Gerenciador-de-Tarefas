using Microsoft.AspNetCore.Mvc;
using TaskManager.TaskApi.Domain.DTO;
using TaskManager.TaskApi.Infra.Repository;

namespace TaskManager.TaskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly TaskRepository _taskRepository;

    public TaskController(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _taskRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await _taskRepository.GetByIdAsync(id));
    }

    [HttpPost("team/{teamId}")]
    public async Task<IActionResult> Post([FromRoute] int teamId, [FromBody] TaskDTO taskDTO)
    {
        await _taskRepository.CreateAsync(teamId,taskDTO);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TaskDTO taskDTO)
    {
        await _taskRepository.EditAsync(id, taskDTO);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _taskRepository.DeleteAsync(id);
        return Ok();
    }
}
