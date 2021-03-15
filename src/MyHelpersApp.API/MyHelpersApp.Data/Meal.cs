using System;

namespace MyHelpersApp.Data
{
    public class Meal
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Comment { get; set; }
        public DateTime Duration { get; set; }
    }
}
