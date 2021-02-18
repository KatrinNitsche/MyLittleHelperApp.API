using Autofac.Extras.Moq;
using MyHelpersApp.API.Controllers;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class ToDoAPIShould
    {
        [Fact]
        public void ReturnToDos()
        {           
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IToDoRepository>()
                    .Setup(x => x.GetAll())
                    .Returns(GetSampleToDos());

                var sut = mock.Create<ToDoController>();

                var todoList = sut.Get();
                Assert.NotNull(todoList);
                Assert.NotEmpty(todoList);
                Assert.Equal(2, todoList.Count());
            }
        }

        [Fact]
        public void AddAToDo()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IToDoRepository>()
                    .Setup(x => x.Add(new ToDo()))
                    .Returns(GetNewSampleToDo());

                var sut = mock.Create<ToDoController>();

                var newToDo = sut.Post(new ToDo());
                Assert.NotNull(newToDo);
            }
        }
              
        private List<ToDo> GetSampleToDos()
        {
            return new List<ToDo>
            {
                new ToDo()
                {
                    Content = "First ToDo",
                    Completed = false,
                    Important = true,
                },
                new ToDo()
                {
                    Content = "Completed Task",
                    Completed = true,
                    Important = false
                }
            };
        }

        private ToDo GetNewSampleToDo()
        {
            return new ToDo()
            {
                Content = "New ToDo",
                Completed = false,
                Important = false
            };
        }
    }
}
