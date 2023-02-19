using TodoList.Data.EF;

namespace TodoList.Web.Middlewares
{
    public class FillRoleValuesMiddleware
    {
        private readonly RequestDelegate _next;

        public FillRoleValuesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DataContext db)
        {
            AddRole("User", db);
            AddRole("Admin", db);

            await _next(context);
        }

        private void AddRole(string roleName, DataContext db)
        {
            var role = db.Roles.FirstOrDefault(x => x.Name == roleName);
            
            if (role == null)
            {
                db.Roles.Add(new Core.Entities.Role { Name = roleName });
                db.SaveChanges();
            }
        }
    }
}
