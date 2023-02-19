using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.DTOs;
using TodoList.Core.Entities;
using TodoList.Data.EF;
using TodoList.Logic.AuthenticationLogics;
using TodoList.Logic.Mappers;
using TodoList.Logic.Services.Implementations;
using TodoList.Logic.Services.Interfaces;
using TodoList.Web.Middlewares;

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
                    options.LoginPath = "/Authentication/Login";
                });

            builder.Services.AddTransient<IAuthenticator, Authenticator>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ITaskService, TaskService>();
            builder.Services.AddTransient<IMapperConvertor<TodoTask, TaskDTO>, TodoTaskToTaskDTOMapper>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
                    template: "{controller=Tasks}/{action=Index}");
            });

            app.UseMiddleware<FillRoleValuesMiddleware>();

            app.Run();
        }
    }
}