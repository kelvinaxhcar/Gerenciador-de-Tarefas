using Microsoft.EntityFrameworkCore;
using TaskManager.TeamApi.Domain.DTO;
using TaskManager.TeamApi.Domain.Models;
using TaskManager.TeamApi.Infra.DB;

namespace TaskManager.TeamApi.Infra.Repository;

public class TeamRepository
{
    private readonly AppDbContext _context;

    public TeamRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(TeamDTO teamDTO)
    {
        await _context.AddAsync(new Team()
        {
            Description = teamDTO.Description,
            Name = teamDTO.Name,
        });
        var id = await _context.SaveChangesAsync();
        _context.Dispose();
        return id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var team = await _context
            .Team
            .FirstOrDefaultAsync(x => x.Id == id);

        if (team is null)
            return false;

        _context.Team.Remove(team);
        await _context.SaveChangesAsync();
        _context.Dispose();
        return true;
    }

    public async Task<Team> GetByIdAsync(int id)
    {
        var team = await _context
            .Team
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Team not found by id {id}");

        _context.Dispose();
        return team;
    }

    public async Task<List<Team>> GetAllAsync()
    {
        var teams = await _context
            .Team
            .ToListAsync();

        _context.Dispose();
        return teams;
    }

    public async Task<bool> EditAsync(int id, TeamDTO teamDTO)
    {
        var teamFound = await _context
            .Team
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Team not found by id {id}");

        teamFound.Name = teamDTO.Name;
        teamFound.Description = teamDTO.Description;
        await _context.SaveChangesAsync();
        _context.Dispose();
        return true;
    }
}

