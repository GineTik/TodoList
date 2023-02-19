using TodoList.Core.DTOs;
using TodoList.Core.Entities;

namespace TodoList.Logic.Mappers
{
    public class TodoTaskToTaskDTOMapper : IMapperConvertor<TodoTask, TaskDTO>
    {
        public TaskDTO Convert(TodoTask from)
        {
            if (from == null) 
                throw new ArgumentNullException(nameof(from));

            return new TaskDTO
            {
                Id = from.Id,
                Completed = from.Completed,
                Text = from.Text,
                ExpirationTime = from.ExpirationTime,
            };
        }
    }
}
