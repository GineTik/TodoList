using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TodoList.Data.EF;

namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc(options =>
                options.EnableEndpointRouting = false);

            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "Authentication/Login";
                });

            var app = builder.Build();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=Login}");
            });

            app.Run();
        }
    }
}