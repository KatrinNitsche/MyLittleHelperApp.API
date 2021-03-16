using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category Add(Category newCategory);
        Category Update(Category category);
    }
}
