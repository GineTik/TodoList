using Microsoft.AspNetCore.Mvc;

namespace TodoList.Web.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
