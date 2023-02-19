using Microsoft.AspNetCore.Mvc;
using TodoList.Core.DTOs;

namespace TodoList.Web.Components
{
    public class TaskViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(TaskDTO task)
        {
            return View(task);
        }
    }
}
