using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TodoList.Core.Entities;

namespace TodoList.Web.Generators
{
    public static class ClaimsIdentityGenerator
    {
        public static ClaimsIdentity Generate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Hash, user.Password),
            };

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
