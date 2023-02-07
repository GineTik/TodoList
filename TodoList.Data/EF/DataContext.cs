using Microsoft.EntityFrameworkCore;
using TodoList.Core.Entities;

namespace TodoList.Data.EF
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) 
        { }
    }
}
