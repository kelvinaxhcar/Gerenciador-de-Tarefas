using Microsoft.AspNetCore.Mvc;
using TaskManager.TeamApi.Domain.DTO;
using TaskManager.TeamApi.Infra.Repository;

namespace TaskManager.TeamApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly TeamRepository _repositoryTeam;

    public TeamController(TeamRepository repositoryTeam)
    {
        _repositoryTeam = repositoryTeam;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TeamDTO teamDTO)
    {
        await _repositoryTeam.CreateAsync(teamDTO);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _repositoryTeam.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var team = _repositoryTeam.GetByIdAsync(id);
        return Ok(team);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teams = await _repositoryTeam.GetAllAsync();
        return Ok(teams);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TeamDTO teamDTO)
    {
        await _repositoryTeam.EditAsync(id, teamDTO);
        return NoContent();
    }
}
