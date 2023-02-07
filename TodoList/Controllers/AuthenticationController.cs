using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
