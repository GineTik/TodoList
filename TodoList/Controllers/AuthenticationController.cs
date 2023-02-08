using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Web.ViewModels.AuthenticationViewModels;

namespace TodoList.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            return RedirectToAction("Index", "Tasks");
        }

        public IActionResult Registration() 
        { 
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            return Redirect("/");
        }
    }
}
