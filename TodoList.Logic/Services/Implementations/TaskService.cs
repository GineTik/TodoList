using Microsoft.EntityFrameworkCore;
using TodoList.Core.DTOs;
using TodoList.Core.Entities;
using TodoList.Data.EF;
using TodoList.Logic.Mappers;
using TodoList.Logic.Services.Interfaces;

namespace TodoList.Logic.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly IMapperConvertor<TodoTask, TaskDTO> _taskMapper;

        public TaskService(DataContext context, IMapperConvertor<TodoTask, TaskDTO> taskMapper)
        {
            _context = context;
            _taskMapper = taskMapper;
        }

        public async Task<TaskDTO> CreateNewTaskAsync(int userId)
        {
            var task = new TodoTask()
            {
                Completed = false,
                Text = "[text]",
                ExpirationTime = DateTime.Now,
                UserId = userId,
                Position = null,
            };
            var result = _context.Tasks.Add(task).Entity;
            await _context.SaveChangesAsync();
            return _taskMapper.Convert(result);
        }

        public Task<TaskDTO?> GetTaskByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                var result = _context.Tasks.FirstOrDefault(x => x.Id == id);

                if (result == null)
                    return null;

                return _taskMapper.Convert(result);
            });
        }

        public async Task<IEnumerable<TaskDTO>> GetUserTasksAsync(int userId)
        {
            return await Task.Run(() =>
            {
                var userTasks = _context.Tasks.Where(x => x.UserId == userId);
                return userTasks.OrderBy(x => x.Position).Select(_taskMapper.Convert);
            });
        }

        public async Task<bool> RemoveUserTaskAsync(int userId, int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task?.UserId != userId)
                return false;

            var result = _context.Tasks.Remove(task).State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateTaskOrderAsync(int userId, int[] taskOrder)
        {
            var tasks = _context.Tasks.Where(x => x.UserId == userId);

            var taskIds = tasks.Select(t => t.Id);
            if (taskOrder.All(taskIds.Contains) == false)
                return false;

            for (int i = 0; i < taskOrder.Count(); i++)
            {
                var task = tasks.First(x => x.Id == taskOrder[i]);
                task.Position = i;
                _context.Entry(task).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserTaskAsync(int userId, TaskDTO dto)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == dto.Id);

            if (task?.UserId != userId)
                return false;

            task.Text = dto.Text;
            task.Completed = dto.Completed;
            task.ExpirationTime = dto.ExpirationTime;

            var result = _context.Tasks.Update(task).State == Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
