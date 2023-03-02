using TodoList.Core.DTOs;
using TodoList.Core.Entities;

namespace TodoList.Logic.Services.Interfaces
{
    public interface ITaskService
    {
        TaskDTO CreateNewTask(int userId);
        IEnumerable<TaskDTO> GetUserTasks(int userId);
        TaskDTO? GetTaskById(int id);
        bool RemoveUserTask(int userId, int taskId);
        bool UpdateUserTask(int userId, TaskDTO dto);
        bool UpdateTaskOrder(int userId, int[] sortedIds);
    }
}
