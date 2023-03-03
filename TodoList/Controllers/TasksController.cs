using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Core.DTOs;
using TodoList.Logic.Services.Interfaces;
using TodoList.Web.Helpers;

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
                int userId = HttpContext.GetLoginedUserId();
                return View(_service.GetUserTasks(userId));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateTask()
        {
            var task = _service.CreateNewTask(HttpContext.GetLoginedUserId());
            return ViewComponent("Task", task);
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveTask(int id)
        {
            var userId = HttpContext.GetLoginedUserId();
            var result = _service.RemoveUserTask(userId, id);
            return Json(result);
        }

        [Authorize]
        public IActionResult UpdateTask(TaskDTO dto)
        {
            var userId = HttpContext.GetLoginedUserId();
            var result = _service.UpdateUserTask(userId, dto);
            return Json(result);
        }

        [Authorize]
        public IActionResult UpdateTaskOrder(int[] sortedIds)
        {
            var userId = HttpContext.GetLoginedUserId();
            var result = _service.UpdateTaskOrder(userId, sortedIds);
            return Json(result);
        }
    }
}
