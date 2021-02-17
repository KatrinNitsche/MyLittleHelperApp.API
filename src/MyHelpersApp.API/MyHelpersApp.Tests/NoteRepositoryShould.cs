using Microsoft.EntityFrameworkCore;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class NoteRepositoryShould
    {
        [Fact]
        public void ReturnNotes()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryData")
                .Options;

            using (var context = new DataContext(options))
            {
                INoteRepository sut = new NoteRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new Note()
                    {
                        Description = $"Description of note {i + 1}",
                        Title = $"Note {i + 1}"
                    });
                }

                var noteList = sut.GetAll().ToList();
                Assert.NotNull(noteList);
                Assert.True(noteList.Any());
            }
        }

        [Fact]
        public void RemoveNote()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase("InMemoryData")
               .Options;

            using (var context = new DataContext(options))
            {
                INoteRepository sut = new NoteRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new Note()
                    {
                        Description = $"Description of note {i + 1}",
                        Title = $"Note {i + 1}"
                    });
                }

                var noteList = sut.GetAll().ToList();

                foreach (var todo in noteList)
                {
                    sut.Remove(todo);
                }

                noteList = sut.GetAll().ToList();
                Assert.NotNull(noteList);
                Assert.False(noteList.Any());
            }
        }

        [Fact]
        public void UpdateNote()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase("InMemoryData")
               .Options;

            using (var context = new DataContext(options))
            {
                INoteRepository sut = new NoteRepository(context);

                for (int i = 0; i < 10; i++)
                {
                    sut.Add(new Note()
                    {
                        Description = $"Description of note {i + 1}",
                        Title = $"Note {i + 1}"
                    });
                }

                var noteList = sut.GetAll().ToList();

                foreach (var note in noteList)
                {
                    note.Description = note.Description + " Updated";
                    sut.Update(note);
                }

                noteList = sut.GetAll().ToList();
                Assert.Contains("Updated", noteList[0].Description);
            }
        }
    }
}
