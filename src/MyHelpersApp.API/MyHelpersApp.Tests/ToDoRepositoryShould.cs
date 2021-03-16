using Microsoft.EntityFrameworkCore;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class ToDoRepositoryShould
    {
        [Fact]
        public void ReturnToDos()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                IToDoRepository sut = new ToDoRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new ToDo()
                    {
                        Content = $"ToDo {i+1}",
                        Completed = false,
                        Important = false
                    });
                }

                var todoList = sut.GetAll(null).ToList();
                Assert.NotNull(todoList);
                Assert.True(todoList.Any());
            }
        }

        [Fact]
        public void RemoveAToDo()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase("InMemoryData")
               .Options;

            using (var context = new DataContext(options))
            {
                IToDoRepository sut = new ToDoRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new ToDo()
                    {
                        Content = $"ToDo {i + 1}",
                        Completed = false,
                        Important = false
                    }); ;
                }

                var todoList = sut.GetAll(null).ToList();

                foreach (var todo in todoList)
                {
                    sut.Remove(todo.Id);
                }

                todoList = sut.GetAll(null).ToList();
                Assert.NotNull(todoList);
                Assert.False(todoList.Any());
            }
        }

        [Fact]
        public void UpdateAToDo()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase("InMemoryData")
               .Options;

            using (var context = new DataContext(options))
            {
                IToDoRepository sut = new ToDoRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new ToDo()
                    {
                        Content = $"ToDo {i + 1}",
                        Completed = false,
                        Important = false
                    });
                }

                var todoList = sut.GetAll(null).ToList();
              
                foreach(var todo in todoList)
                {
                    todo.Content = todo.Content + " Updated";
                    todo.Completed = true;
                    sut.Update(todo);
                }

                todoList = sut.GetAll(null).ToList();

                Assert.Contains("Updated", todoList[0].Content);
                Assert.True(todoList[0].Completed);
            }
        }

        [Fact]
        public void ReturnAListOfToDosForACategory()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase("InMemoryData")
              .Options;

            using (var context = new DataContext(options))
            {
                ICategoryRepository categoryRepository = new CategoryRepository(context);
                var category = new Category()
                {
                    Colour = "#101010",
                    Name = "Test Category"
                };
                categoryRepository.Add(category);


                IToDoRepository sut = new ToDoRepository(context);

                for (int i = 0; i < 3; i++)
                {
                    sut.Add(new ToDo()
                    {
                        Content = $"ToDo {i + 1}",
                        Completed = false,
                        Important = false,
                        CategoryId = category.Id
                    });
                }

                var todoList = sut.GetAll(category.Id).ToList();
                Assert.NotNull(todoList);
                Assert.True(todoList.Count == 3);
            }
        }
    }
}
