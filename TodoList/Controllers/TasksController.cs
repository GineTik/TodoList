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

        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                int userId = HttpContext.GetLoginedUserId();
                return View(await _service.GetUserTasksAsync(userId));
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTask()
        {
            var task = await _service.CreateNewTaskAsync(HttpContext.GetLoginedUserId());
            return ViewComponent("Task", task);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveTask(int id)
        {
            var userId = HttpContext.GetLoginedUserId();
            var result = await _service.RemoveUserTaskAsync(userId, id);
            return Json(result);
        }

        [Authorize]
        public async Task<IActionResult> UpdateTask(TaskDTO dto)
        {
            var userId = HttpContext.GetLoginedUserId();
            var result = await _service.UpdateUserTaskAsync(userId, dto);
            return Json(result);
        }

        [Authorize]
        public async Task<IActionResult> UpdateTaskOrder(int[] sortedIds)
        {
            var userId = HttpContext.GetLoginedUserId();
            var result = await _service.UpdateTaskOrderAsync(userId, sortedIds);
            return Json(result);
        }
    }
}
