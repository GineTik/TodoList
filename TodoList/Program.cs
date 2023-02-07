namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc(options =>
                options.EnableEndpointRouting = false);

            var app = builder.Build();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=Index}");
            });

            //app.UseMvc();
            //app.MapControllerRoute(
            //    "default",
            //    "{controller}/{action=Index}");

            app.Run();
        }
    }
}