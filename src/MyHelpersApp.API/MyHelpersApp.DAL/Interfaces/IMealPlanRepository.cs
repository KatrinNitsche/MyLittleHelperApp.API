using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Interfaces
{
    public interface IMealPlanRepository
    {
        IEnumerable<MealPlanDay> LoadPlan();
        MealPlanDay Add(MealPlanDay newWeekDay);
    }
}
