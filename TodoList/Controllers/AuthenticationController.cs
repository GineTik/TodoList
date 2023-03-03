using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Core.DTOs;
using TodoList.Core.Entities;
using TodoList.Logic.Services.Interfaces;
using TodoList.Web.Generators;
using TodoList.Web.Helpers;
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

            var user = _userService.TryLogin(new UserDTO { Login = model.Login, Password = model.Password });

            if (user == null)
            {
                ModelState.AddModelError("UserNotRegistred", "Користувач не знайдений");
                return View(model);
            }
            else
            {
                await HttpContext.SignInAsync(user);
                return RedirectToAction("Index", "Tasks");
            }
        }

        public IActionResult LoginByGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(LoginByGoogleCallback))
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> LoginByGoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (authenticateResult.Succeeded == false)
                return RedirectToAction(nameof(Login));

            var claims = authenticateResult.Principal.Claims.ToList();
            var externalEmail = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var dto = new UserDTO
            {
                Login = externalEmail,
                Password = externalEmail + "__password"
            };

            var user = _userService.TryLogin(dto);
            if (user == null)
                user = _userService.TryRegistration(dto);

            await HttpContext.SignInAsync(user);
            return RedirectToAction("Index", "Tasks");
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

            var user = _userService.TryRegistration(new UserDTO { Login = model.Login, Password = model.Password });

            if (user == null)
            {
                ModelState.AddModelError("UserRegistred", "Користувач зареєстрований");
                return View(model);
            }
            else
            {
                await HttpContext.SignInAsync(user);
                return RedirectToAction("Index", "Tasks");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
