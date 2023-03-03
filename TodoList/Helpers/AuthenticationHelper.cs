using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TodoList.Core.Entities;
using TodoList.Web.Generators;

namespace TodoList.Web.Helpers
{
    public static class AuthenticationHelper
    {
        public static async Task SignInAsync(this HttpContext context, User user)
        {
            var identity = ClaimsIdentityGenerator.Generate(user);
            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );
        }

        public static int GetLoginedUserId(this HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated == false)
                throw new InvalidOperationException("user not authenticated");

            int userId = int.Parse(context.User.Claims
                    .First(c => c.Type == ClaimTypes.NameIdentifier)
                    .Value);

            return userId;
        }
    }
}
