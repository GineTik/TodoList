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

        public TaskDTO CreateNewTask(int userId)
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
            _context.SaveChanges();
            return _taskMapper.Convert(result);
        }

        public TaskDTO? GetTaskById(int id)
        {
            var result = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (result == null) 
                return null;

            return _taskMapper.Convert(result);
        }

        public IEnumerable<TaskDTO> GetUserTasks(int userId)
        {
            var userTasks = _context.Tasks.Where(x => x.UserId == userId);
            return userTasks.OrderBy(x => x.Position).Select(_taskMapper.Convert);
        }

        public bool RemoveUserTask(int userId, int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task?.UserId != userId)
                return false;

            var result = _context.Tasks.Remove(task).State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return result;
        }

        public bool UpdateTaskOrder(int userId, int[] taskOrder)
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

            _context.SaveChanges();
            return true;
        }

        public bool UpdateUserTask(int userId, TaskDTO dto)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == dto.Id);

            if (task?.UserId != userId)
                return false;

            task.Text = dto.Text;
            task.Completed = dto.Completed;
            task.ExpirationTime = dto.ExpirationTime;

            var result = _context.Tasks.Update(task).State == Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return result;
        }
    }
}
