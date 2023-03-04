using TodoList.Core.DTOs;

namespace TodoList.Logic.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDTO> CreateNewTaskAsync(int userId);
        Task<IEnumerable<TaskDTO>> GetUserTasksAsync(int userId);
        Task<TaskDTO?> GetTaskByIdAsync(int id);
        Task<bool> RemoveUserTaskAsync(int userId, int taskId);
        Task<bool> UpdateUserTaskAsync(int userId, TaskDTO dto);
        Task<bool> UpdateTaskOrderAsync(int userId, int[] sortedIds);
    }
}
