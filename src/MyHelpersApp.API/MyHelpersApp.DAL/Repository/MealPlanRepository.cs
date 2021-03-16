using Microsoft.EntityFrameworkCore;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.DAL.Repository
{
    public class MealPlanRepository : IMealPlanRepository
    {
        private DataContext context;

        public MealPlanRepository(DataContext context)
        {
            this.context = context;
        }

        public MealPlanDay Add(MealPlanDay newWeekDay)
        {
            var existingWeekday = context.MealPlanDays.FirstOrDefault(d => d.WeekDayName == newWeekDay.WeekDayName);
            if (existingWeekday == null)
            {
                existingWeekday = new MealPlanDay()
                {
                    WeekDayName = newWeekDay.WeekDayName,
                    Comment = newWeekDay.Comment,
                    Meals = newWeekDay.Meals                   
                };
                
                context.Add(existingWeekday);
            }
            else
            {
                existingWeekday.Comment = newWeekDay.Comment;
                existingWeekday.Meals = newWeekDay.Meals;
                existingWeekday.WeekDayName = newWeekDay.WeekDayName;
            }
            
            context.SaveChanges();
            return existingWeekday;
        }

        public IEnumerable<MealPlanDay> LoadPlan()
        {
            if (this.context.MealPlanDays.Count() == 0)
            {
                var weekDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                foreach (var item in weekDays)
                {
                    var newWeekDay = new MealPlanDay()
                    {
                        WeekDayName = item,
                        Meals = new List<Meal>()
                        {
                            new Meal() {MealName = "Meal 1", Comment = "Comment for meal 1", Duration = new System.DateTime() },
                            new Meal() {MealName = "Meal 2", Comment = "Comment for meal 1", Duration = new System.DateTime() },
                            new Meal() {MealName = "Meal 3", Comment = "Comment for meal 1", Duration = new System.DateTime() }
                        }
                    };

                    context.MealPlanDays.Add(newWeekDay);
                    context.SaveChanges();
                }
            }

            return this.context.MealPlanDays.Include(x => x.Meals);
        }

        public IEnumerable<MealPlanDay> Reset()
        {
            var allEntries = this.context.MealPlanDays.Include(x => x.Meals);
            foreach (var entry in allEntries)
            {
                foreach (var meal in entry.Meals) 
                {
                    this.context.Meals.Remove(meal);
                }
                this.context.Remove(entry);
            }

            context.SaveChanges();

            var weekDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            foreach (var item in weekDays)
            {
                var newWeekDay = new MealPlanDay()
                {
                    WeekDayName = item,
                    Meals = new List<Meal>()
                        {
                            new Meal() {MealName = "Meal 1", Comment = "Comment for meal 1", Duration = new System.DateTime() },
                            new Meal() {MealName = "Meal 2", Comment = "Comment for meal 1", Duration = new System.DateTime() },
                            new Meal() {MealName = "Meal 3", Comment = "Comment for meal 1", Duration = new System.DateTime() }
                        }
                };

                context.MealPlanDays.Add(newWeekDay);
                context.SaveChanges();
            }

            return this.context.MealPlanDays.Include(x => x.Meals);
        }
    }
}
