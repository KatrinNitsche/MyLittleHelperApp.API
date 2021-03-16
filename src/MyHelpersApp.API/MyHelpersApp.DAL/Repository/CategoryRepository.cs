using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext context;

        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public Category Add(Category newCategory)
        {
            newCategory.Created = DateTime.Now;
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return newCategory;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category Update(Category category)
        {
            var entry = context.Categories.Find(category.Id);
            if (entry != null)
            {
                entry.Colour = category.Colour;
                entry.Updated = DateTime.Now;
                entry.Name = category.Name;
                entry.Symbol = category.Symbol;
                context.SaveChanges();
            }

            return entry;
        }
    }
}
