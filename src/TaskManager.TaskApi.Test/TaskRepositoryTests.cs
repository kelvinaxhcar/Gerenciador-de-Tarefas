using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.TaskApi.Domain.DTO;
using TaskManager.TaskApi.Domain.Models;
using TaskManager.TaskApi.Infra.DB;
using TaskManager.TaskApi.Infra.Repository;

namespace TaskManager.TeamApi.Test;

public class TaskRepositoryTests : BaseTest {
  [Fact]
  public async void Add_Task_Should_Add_Task_In_Database() {
    var serviceProvider =
        CreateServices(nameof(Add_Task_Should_Add_Task_In_Database));

    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var repository = scope.ServiceProvider.GetRequiredService<TaskRepository>();

    var task = new TaskDTO { Titulo = "Task A", Description = "Description A" };
    await repository.CreateAsync(1, task);

    var teams = await context.Task.ToListAsync();
    Assert.Single(teams);
  }

  [Fact]
  public async void Delete_Task_Should_Delete_Task_In_Database() {
    var serviceProvider =
        CreateServices(nameof(Delete_Task_Should_Delete_Task_In_Database));

    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var repository = scope.ServiceProvider.GetRequiredService<TaskRepository>();

    var task = new TaskApi.Domain.Models.Task() { Description = "Test desc",
                                                  Title = "Test" };

    await context.AddAsync(task);
    await context.SaveChangesAsync();

    await repository.DeleteAsync(task.Id);

    var tasks = await context.Task.ToListAsync();
    Assert.Empty(tasks);
  }

  [Fact]
  public async void Get_All_Tasks_Must_Get_All_Tasks_From_Database() {
    var serviceProvider =
        CreateServices(nameof(Get_All_Tasks_Must_Get_All_Tasks_From_Database));

    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var repository = scope.ServiceProvider.GetRequiredService<TaskRepository>();

    var task1 = new TaskApi.Domain.Models.Task() { Description = "Test desc1",
                                                   Title = "Test1" };

    var task2 = new TaskApi.Domain.Models.Task() { Description = "Test desc2",
                                                   Title = "Test2" };

    await context.AddAsync(task1);
    await context.AddAsync(task2);
    await context.SaveChangesAsync();

    var tasks = await repository.GetAllAsync();

    Assert.Equal(2, tasks.Count);
  }

  [Fact]
  public async void Get_By_Id_Should_Get_Task_By_Id_From_Database() {
    var serviceProvider =
        CreateServices(nameof(Get_By_Id_Should_Get_Task_By_Id_From_Database));

    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var repository = scope.ServiceProvider.GetRequiredService<TaskRepository>();

    var task1 = new TaskApi.Domain.Models.Task() { Description = "Test desc1",
                                                   Title = "Test1" };

    var task2 = new TaskApi.Domain.Models.Task() { Description = "Test desc2",
                                                   Title = "Test2" };

    await context.AddAsync(task1);
    await context.AddAsync(task2);
    await context.SaveChangesAsync();

    var task = await repository.GetByIdAsync(task2.Id);

    Assert.Equal("Test2", task.Title);
    Assert.Equal("Test desc2", task.Description);
  }

  [Fact]
  public async void Edit_Task_Must_Edit_Task_Database() {
    var serviceProvider =
        CreateServices(nameof(Edit_Task_Must_Edit_Task_Database));

    using var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var repository = scope.ServiceProvider.GetRequiredService<TaskRepository>();

    var task1 = new TaskApi.Domain.Models.Task() { Description = "Test desc1",
                                                   Title = "Test1" };

    var task2 = new TaskApi.Domain.Models.Task() { Description = "Test desc2",
                                                   Title = "Test2" };

    await context.AddAsync(task1);
    await context.AddAsync(task2);
    await context.SaveChangesAsync();

    await repository.EditAsync(
        task2.Id,
        new TaskDTO() { Description = "Test desc3", Titulo = "Test3" });

    var task =
        await context.Task.Where(x => x.Id == task2.Id).FirstOrDefaultAsync();

    Assert.NotNull(task);
    Assert.Equal("Test3", task.Title);
    Assert.Equal("Test desc3", task.Description);
  }
}