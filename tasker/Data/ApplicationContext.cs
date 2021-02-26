using Microsoft.EntityFrameworkCore;
using tasker.Models;
namespace tasker.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<tasker.Models.TaskModel.TaskManager> TaskManager { get; set; }
    }
}
