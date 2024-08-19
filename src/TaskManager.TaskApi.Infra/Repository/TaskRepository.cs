using Microsoft.EntityFrameworkCore;
using TaskManager.TaskApi.Domain.DTO;
using TaskManager.TaskApi.Infra.DB;

namespace TaskManager.TaskApi.Infra.Repository;

public class TaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(int teamId, TaskDTO taskDTO)
    {
        await _context.AddAsync(new Domain.Models.Task()
        {
            Description = taskDTO.Description,
            TeamId = teamId,
            Completed = taskDTO.Concluida,
            Title = taskDTO.Titulo
        });
        var id = await _context.SaveChangesAsync();
        return id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context
            .Task
            .FirstOrDefaultAsync(x => x.Id == id);

        if (task is null)
            return false;

        _context.Task.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Domain.Models.Task> GetByIdAsync(int id)
    {
        var task = await _context
            .Task
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Task not found by id {id}");
        return task;
    }

    public async Task<List<Domain.Models.Task>> GetAllAsync()
    {
        var tasks = await _context
            .Task
            .ToListAsync();
        return tasks;
    }

    public async Task<bool> EditAsync(int id, TaskDTO taskDTO)
    {
        var task = await _context
            .Task
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Task not found by id {id}");

        task.Description = taskDTO.Description;
        task.Title = taskDTO.Titulo;
        task.Completed = taskDTO.Concluida;

        await _context.SaveChangesAsync();

        return true;
    }
}

