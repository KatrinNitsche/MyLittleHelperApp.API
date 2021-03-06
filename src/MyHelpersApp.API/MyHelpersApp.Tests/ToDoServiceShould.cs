﻿using Microsoft.EntityFrameworkCore;
using MyHelpersApp.BL.Services;
using MyHelpersApp.DAL;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.DAL.Repository;
using MyHelpersApp.Data;
using System;
using System.Linq;
using Xunit;

namespace MyHelpersApp.Tests
{
    public class ToDoServiceShould
    {
        [Fact]
        public void ReturnToDos()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
              .UseInMemoryDatabase("InMemoryData")
              .Options;

            using (var context = new DataContext(options))
            {
                IToDoRepository todoRepository = new ToDoRepository(context);
                var sut = new ToDoService(todoRepository);

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
                IToDoRepository todoRepository = new ToDoRepository(context);
                var sut = new ToDoService(todoRepository);

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
                IToDoRepository todoRepository = new ToDoRepository(context);
                var sut = new ToDoService(todoRepository);

                sut.Add(new ToDo()
                {
                    Content = $"Daily ToDo",
                    Completed = false,
                    Important = false,
                    DueDate = DateTime.Now,
                    RepetitionType = 1
                });

                var todoList = sut.GetAll(null).ToList();

                var toDoToChange = new ToDo()
                {
                    Completed = todoList[0].Completed,
                    Content = todoList[0].Content,
                    Created = todoList[0].Created,
                    DueDate = todoList[0].DueDate,
                    Id = todoList[0].Id,
                    Important = todoList[0].Important,
                    RepetitionType = todoList[0].RepetitionType,
                    Updated = todoList[0].Updated
                };

                toDoToChange.Completed = true;
                var changed = sut.Update(toDoToChange);

                Assert.Equal(1, changed.RepetitionType);
                Assert.False(changed.Completed);
                Assert.Equal(DateTime.Now.Date.AddDays(1), changed.DueDate.Date);

            }
        }
    }
}
