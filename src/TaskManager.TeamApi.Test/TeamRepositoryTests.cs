using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.TeamApi.Domain.DTO;
using TaskManager.TeamApi.Domain.Models;
using TaskManager.TeamApi.Infra.DB;
using TaskManager.TeamApi.Infra.Repository;

namespace TaskManager.TeamApi.Test;

public class TeamRepositoryTests : BaseTest
{
    [Fact]
    public async void AddTeam_Should_Add_Team_In_Database()
    {
        var serviceProvider = CreateServices(nameof(AddTeam_Should_Add_Team_In_Database));

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<TeamRepository>();

        var team = new TeamDTO { Name = "Team A", Description = "Description A" };
        await repository.CreateAsync(team);

        var teams = await context
            .Team
            .ToListAsync();
        Assert.Single(teams);
    }

    [Fact]
    public async void Delete_Team_Should_Delete_Team_In_Database()
    {
        var serviceProvider = CreateServices(nameof(Delete_Team_Should_Delete_Team_In_Database));

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<TeamRepository>();

        var team = new Team()
        {
            Description = "Test desc",
            Name = "Test"
        };

        await context.AddAsync(team);
        await context.SaveChangesAsync();

        await repository.DeleteAsync(team.Id);

        var teams = await context
           .Team
           .ToListAsync();
        Assert.Empty(teams);
    }

    [Fact]
    public async void Get_All_Teams_Must_Get_All_Teams_From_Database()
    {
        var serviceProvider = CreateServices(nameof(Get_All_Teams_Must_Get_All_Teams_From_Database));

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<TeamRepository>();

        var team1 = new Team()
        {
            Description = "Test desc1",
            Name = "Test1"
        };

        var team2 = new Team()
        {
            Description = "Test desc2",
            Name = "Test2"
        };

        await context.AddAsync(team1);
        await context.AddAsync(team2);
        await context.SaveChangesAsync();

        var teams = await repository.GetAllAsync();

        Assert.Equal(2, teams.Count);
    }

    [Fact]
    public async void Get_By_Id_Should_Get_Team_By_Id_From_Database()
    {
        var serviceProvider = CreateServices(nameof(Get_By_Id_Should_Get_Team_By_Id_From_Database));

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<TeamRepository>();

        var team1 = new Team()
        {
            Description = "Test desc1",
            Name = "Test1"
        };

        var team2 = new Team()
        {
            Description = "Test desc2",
            Name = "Test2"
        };

        await context.AddAsync(team1);
        await context.AddAsync(team2);
        await context.SaveChangesAsync();

        var team = await repository.GetByIdAsync(team2.Id);

        Assert.Equal("Test2", team.Name);
        Assert.Equal("Test desc2", team.Description);
    }

    [Fact]
    public async void Edit_Team_Must_Edit_Team_Database()
    {
        var serviceProvider = CreateServices(nameof(Edit_Team_Must_Edit_Team_Database));

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<TeamRepository>();

        var team1 = new Team()
        {
            Description = "Test desc1",
            Name = "Test1"
        };

        var team2 = new Team()
        {
            Description = "Test desc2",
            Name = "Test2"
        };

        await context.AddAsync(team1);
        await context.AddAsync(team2);
        await context.SaveChangesAsync();

        await repository.EditAsync(team2.Id, new TeamDTO()
        {
            Description = "Test desc3",
            Name = "Test3"
        });

        var team = await context
            .Team
            .Where(x=> x.Id == team2.Id)
            .FirstOrDefaultAsync();

        Assert.NotNull(team);
        Assert.Equal("Test3", team.Name);
        Assert.Equal("Test desc3", team.Description);
    }
}