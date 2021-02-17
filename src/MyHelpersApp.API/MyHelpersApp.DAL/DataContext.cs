using Microsoft.EntityFrameworkCore;
using MyHelpersApp.Data;

namespace MyHelpersApp.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
