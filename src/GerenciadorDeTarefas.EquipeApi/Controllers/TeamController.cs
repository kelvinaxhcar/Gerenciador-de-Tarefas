using Microsoft.AspNetCore.Mvc;
using TaskManager.TeamApi.DTO;
using TaskManager.TeamApi.Infra.Repository;

namespace TaskManager.TeamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly RepositoryTeam _repositoryTeam;

        public TeamController(RepositoryTeam repositoryTeam)
        {
            _repositoryTeam = repositoryTeam;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TeamDTO teamDTO)
        {
            await _repositoryTeam.Create(teamDTO);
            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult Delet([FromRoute] int id)
        {
            _repositoryTeam.Delete(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var team = _repositoryTeam.GetById(id);
            return Ok(team);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var teams = _repositoryTeam.GetAll();
            return Ok(teams);
        }

        [HttpPatch("{id}")]
        public IActionResult Edit([FromRoute] int id, [FromBody] TeamDTO teamDTO)
        {
            _repositoryTeam.Edit(id, teamDTO);
            return Ok();
        }
    }
}
