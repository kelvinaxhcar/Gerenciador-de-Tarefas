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
    public IActionResult Delet([FromRoute] int id)
    {
        _repositoryTeam.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var team = _repositoryTeam.GetByIdAsync(id);
        return Ok(team);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var teams = _repositoryTeam.GetAllAsync();
        return Ok(teams);
    }

    [HttpPatch("{id}")]
    public IActionResult Edit([FromRoute] int id, [FromBody] TeamDTO teamDTO)
    {
        _repositoryTeam.EditAsync(id, teamDTO);
        return Ok();
    }
}
