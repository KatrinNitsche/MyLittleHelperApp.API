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

                var todoList = sut.GetAll().ToList();
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
                    });
                }

                var todoList = sut.GetAll().ToList();

                foreach (var todo in todoList)
                {
                    sut.Remove(todo);
                }

                todoList = sut.GetAll().ToList();
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

                var todoList = sut.GetAll().ToList();
              
                foreach(var todo in todoList)
                {
                    todo.Completed = true;
                    sut.Update(todo);
                }
            }
        }
    }
}
