using Microsoft.AspNetCore.Mvc;
using TaskManager.TaskApi.Domain.DTO;
using TaskManager.TaskApi.Domain.Services;

namespace TaskManager.TaskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase {
  private readonly TaskService _taskService;

  public TaskController(TaskService taskService) { _taskService = taskService; }

  [HttpGet]
  public async Task<IActionResult> Get() {
    return Ok(await _taskService.GetAllAsync());
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get([FromRoute] int id) {
    return Ok(await _taskService.GetByIdAsync(id));
  }

  [HttpPost("team/{teamId}")]
  public async Task<IActionResult> Post([FromRoute] int teamId,
                                        [FromBody] TaskDTO taskDTO) {
    await _taskService.CreateAsync(teamId, taskDTO);
    return Created();
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Put([FromRoute] int id,
                                       [FromBody] TaskDTO taskDTO) {
    await _taskService.EditAsync(id, taskDTO);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete([FromRoute] int id) {
    await _taskService.DeleteAsync(id);
    return NoContent();
  }
}
