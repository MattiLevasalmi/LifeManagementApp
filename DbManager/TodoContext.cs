using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.DbManager
{
    public class TodoContext : DbContext
    {
        public DbSet<Tasks> TaskList { get; set; }
        public DbSet<Task> Task { get; set; }

        public string DbPath { get; }

        public TodoContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "todo.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}