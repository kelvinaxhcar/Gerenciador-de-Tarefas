using GerenciadorDeTarefas.EquipeApi.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using TaskManager.TeamApi.DTO;
using TaskManager.TeamApi.Infra.DB;

namespace TaskManager.TeamApi.Infra.Repository
{
    public class RepositoryTeam
    {

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public RepositoryTeam(DbContextOptions<AppDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task<int> Create(TeamDTO teamDTO)
        {
            using var context = new AppDbContext(_dbContextOptions);
            await context.AddAsync(new Team()
            {
                Description = teamDTO.Description,
                Name = teamDTO.Name,
            });
            return context.SaveChanges();
        }

        public void Delete(int id)
        {
            using var context = new AppDbContext(_dbContextOptions);
            var team = context
                .Team
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (team is null)
                return;
            context.Team.Remove(team);
            context.SaveChanges();
        }

        public Team GetById(int id)
        {
            using var context = new AppDbContext(_dbContextOptions);
            return context
                .Team
                .Where(x => x.Id == id)
                .FirstOrDefault() ?? throw new Exception($"Team not found by id {id}");
        }

        public List<Team> GetAll()
        {
            using var context = new AppDbContext(_dbContextOptions);
            return context
                .Team
                .ToList();
        }

        public void Edit(int id, TeamDTO team)
        {
            using var context = new AppDbContext(_dbContextOptions);
            var teamFound = context
                .Team
                .Where(x => x.Id == id)
                .FirstOrDefault() ?? throw new Exception($"Team not found by id {id}");

            teamFound.Name = team.Name;
            teamFound.Description = team.Description;
            context.SaveChanges();
        }
    }
}
