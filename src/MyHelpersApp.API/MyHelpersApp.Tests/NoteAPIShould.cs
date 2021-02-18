using Autofac.Extras.Moq;
using MyHelpersApp.API.Controllers;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class NoteAPIShould
    {
        [Fact]
        public void ReturnNotes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<INoteRepository>()
                    .Setup(x => x.GetAll())
                    .Returns(GetSampleNotes());

                var sut = mock.Create<NoteController>();

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
                mock.Mock<INoteRepository>()
                    .Setup(x => x.Add(new Note()))
                    .Returns(GetNewSampleNote());

                var sut = mock.Create<NoteController>();

                var newToDo = sut.Post(new Note());
                Assert.NotNull(newToDo);
            }
        }

        private Note GetNewSampleNote()
        {
            return new Note()
            {
                Title = "New Note",
                Description = "Description of the new note."
            };
        }

        private List<Note> GetSampleNotes()
        {
            return new List<Note>
            {
                new Note()
                {
                    Title = "Sample Title",
                    Description = "Sample description of the sample note."
                },
                new Note()
                {
                    Title = "Second Note",
                    Description = "This is the second note"
                }
            };
        }
    }
}
