using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return this.categoryRepository.GetAll();
        }

        [HttpPost]
        public Category Post(Category category)
        {
            if (category.Id == 0)
            {
                this.categoryRepository.Add(category);
            }
            else
            {
                this.categoryRepository.Update(category);
            }

            return category;
        }
    }
}