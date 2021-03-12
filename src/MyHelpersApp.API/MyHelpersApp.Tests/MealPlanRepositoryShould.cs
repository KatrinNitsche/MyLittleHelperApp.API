using Microsoft.EntityFrameworkCore;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class MealPlanRepositoryShould
    {
        [Fact]
        public void ReturnAFullWeek()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase("InMemoryData")
              .Options;

            using (var context = new DataContext(options))
            {
                IMealPlanRepository sut = new MealPlanRepository(context);
                List<MealPlanDay> plan = sut.LoadPlan().ToList();

                Assert.Equal(7, plan.Count);
                Assert.Equal("Monday", plan[0].WeekDayName);
                Assert.Equal("Tuesday", plan[1].WeekDayName);
                Assert.Equal("Wednesday", plan[2].WeekDayName);
                Assert.Equal("Thursday", plan[3].WeekDayName);
                Assert.Equal("Friday", plan[4].WeekDayName);
                Assert.Equal("Saturday", plan[5].WeekDayName);
                Assert.Equal("Sunday", plan[6].WeekDayName);
            }
        }

        [Fact]
        public void UpdateMealForDay()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                         .UseInMemoryDatabase("InMemoryData")
                         .Options;

            using (var context = new DataContext(options))
            {
                IMealPlanRepository sut = new MealPlanRepository(context);
                List<MealPlanDay> plan = sut.LoadPlan().ToList();
                var monday = plan[0];
                monday.Meals[0].MealName = "Updated Meal 1";
                monday.Meals[0].Comment = "Updated comment for meal 1";
                sut.Add(monday);

                List<MealPlanDay> newPlan = sut.LoadPlan().ToList();
                Assert.Equal(7, newPlan.Count);
                Assert.Equal("Monday", newPlan[0].WeekDayName);
                Assert.Equal("Tuesday", newPlan[1].WeekDayName);
                Assert.Equal("Wednesday", newPlan[2].WeekDayName);
                Assert.Equal("Thursday", newPlan[3].WeekDayName);
                Assert.Equal("Friday", newPlan[4].WeekDayName);
                Assert.Equal("Saturday", newPlan[5].WeekDayName);
                Assert.Equal("Sunday", newPlan[6].WeekDayName);

                Assert.Equal("Updated Meal 1", newPlan[0].Meals[0].MealName);
                Assert.Equal("Updated comment for meal 1", newPlan[0].Meals[0].Comment);
            }
        }
    }
}
