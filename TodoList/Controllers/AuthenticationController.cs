using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Core.DTOs;
using TodoList.Logic.Services.Interfaces;
using TodoList.Web.Generators;
using TodoList.Web.ViewModels.AuthenticationViewModels;

namespace TodoList.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var user = _userService.Login(new UserDTO { Login = model.Login, Password = model.Password });

            if (user == null)
            {
                ModelState.AddModelError("UserNotRegistred", "Користувач не знайдений");
                return View(model);
            }
            else
            {
                var identity = ClaimsIdentityGenerator.Generate(user);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                );
                return RedirectToAction("Index", "Tasks");
            }
        }

        public IActionResult Registration() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var user = _userService.Registration(new UserDTO { Login = model.Login, Password = model.Password });

            if (user == null)
            {
                ModelState.AddModelError("UserRegistred", "Користувач зареєстрований");
                return View(model);
            }
            else
            {
                var identity = ClaimsIdentityGenerator.Generate(user);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                );
                return RedirectToAction("Index", "Tasks");
            }
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
