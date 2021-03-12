using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealPlanController
    {
        private readonly IMealPlanRepository mealPlanRepository;

        public MealPlanController(IMealPlanRepository mealPlanRepository)
        {
            this.mealPlanRepository = mealPlanRepository;
        }

        [HttpGet]
        public IEnumerable<MealPlanDay> Get()
        {
            return this.mealPlanRepository.LoadPlan();
        }

        [HttpPost]
        public MealPlanDay Post(MealPlanDay mealPlanDay)
        {
            return this.mealPlanRepository.Add(mealPlanDay);
        }
    }
}
