using System.Collections.Generic;

namespace MyHelpersApp.Data
{
    public class MealPlanDay
    {
        public int Id { get; set; }
        public string WeekDayName { get; set; }
        public string Comment { get; set; }

        public List<Meal> Meals { get; set; }
    }
}
