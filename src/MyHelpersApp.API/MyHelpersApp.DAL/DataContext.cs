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
        public DbSet<BudgetEntry> BudgetEntries { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<MealPlanDay> MealPlanDays { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
