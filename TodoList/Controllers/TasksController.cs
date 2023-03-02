using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Core.DTOs;
using TodoList.Logic.Services.Interfaces;

namespace TodoList.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                int userId = GetLoginedUserId();
                return View(_service.GetUserTasks(userId));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateTask()
        {
            var task = _service.CreateNewTask(GetLoginedUserId());
            return ViewComponent("Task", task);
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveTask(int id)
        {
            var userId = GetLoginedUserId();
            var result = _service.RemoveUserTask(userId, id);
            return Json(result);
        }

        [Authorize]
        public IActionResult UpdateTask(TaskDTO dto)
        {
            var userId = GetLoginedUserId();
            var result = _service.UpdateUserTask(userId, dto);
            return Json(result);
        }

        [Authorize]
        public IActionResult UpdateTaskOrder(int[] sortedIds)
        {
            var userId = GetLoginedUserId();
            var result = _service.UpdateTaskOrder(userId, sortedIds);
            return Json(result);
        }

        private int GetLoginedUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
                throw new InvalidOperationException("user not authenticated");

            int userId = int.Parse(HttpContext.User.Claims
                    .First(c => c.Type == ClaimTypes.NameIdentifier)
                    .Value);

            return userId;
        }
    }
}
