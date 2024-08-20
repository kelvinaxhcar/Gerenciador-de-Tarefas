
using TaskManager.TaskApi.Domain.DTO;
using TaskManager.TaskApi.Infra.Repository;

namespace TaskManager.TaskApi.Domain.Services {
public class TaskService {
  private readonly TaskRepository _taskRepository;

  public TaskService(TaskRepository taskRepository) {
    _taskRepository = taskRepository;
  }

  public async Task<int> CreateAsync(int teamId, TaskDTO taskDTO) {
    return await _taskRepository.CreateAsync(teamId, taskDTO);
  }

  public async Task DeleteAsync(int id) {
    await _taskRepository.DeleteAsync(id);
  }

  public async Task EditAsync(int id, TaskDTO taskDTO) {
    await _taskRepository.EditAsync(id, taskDTO);
  }

  public async Task<List<Models.Task>> GetAllAsync() {
    return await _taskRepository.GetAllAsync();
  }

  public async Task<Models.Task> GetByIdAsync(int id) {
    return await _taskRepository.GetByIdAsync(id);
  }
}
}
