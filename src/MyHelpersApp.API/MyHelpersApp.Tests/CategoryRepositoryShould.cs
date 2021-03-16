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
    public class CategoryRepositoryShould
    {
        [Fact]
        public void ReturnAllCategories()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase("InMemoryData")
             .Options;

            using (var context = new DataContext(options))
            {
                ICategoryRepository sut = new CategoryRepository(context);
                for (int i = 0; i < 10; i++)
                {
                    var newCategory = new Category()
                    {
                        Name = $"Category {i + 1}",
                        Symbol = "",
                        Colour = ""
                    };

                    sut.Add(newCategory);
                }

                List<Category> listOfCategories = sut.GetAll().ToList();
                Assert.NotNull(listOfCategories);
                Assert.True(listOfCategories.Count >= 10);
            }
        }

        [Fact]
        public void AddACategory()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase("InMemoryData")
           .Options;

            using (var context = new DataContext(options))
            {
                ICategoryRepository sut = new CategoryRepository(context);
               
                var newCategory = new Category()
                {
                    Name = $"New Category",
                    Symbol = "",
                    Colour = ""
                };

                sut.Add(newCategory);               

                List<Category> listOfCategories = sut.GetAll().ToList();
                Assert.NotNull(listOfCategories);
                Assert.Equal("New Category", listOfCategories.Last().Name);
            }
        }

        [Fact]
        public void UpdateACategory()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                ICategoryRepository sut = new CategoryRepository(context);

                var newCategory = new Category()
                {
                    Name = $"New Category",
                    Symbol = "",
                    Colour = ""
                };

                sut.Add(newCategory);

                List<Category> listOfCategories = sut.GetAll().ToList();
                var lastCategory = listOfCategories.Last();
                lastCategory.Name = "Updated Name";
                var check = sut.Update(lastCategory);

                listOfCategories = sut.GetAll().ToList();
                Assert.NotNull(check);
                Assert.NotNull(listOfCategories);
                Assert.Equal("Updated Name", listOfCategories.Last().Name);
            }
        }
    }
}
