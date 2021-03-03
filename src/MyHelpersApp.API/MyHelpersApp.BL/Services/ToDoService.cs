using MyHelpersApp.BL.Interfaces;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.BL.Services
{
    public class ToDoService : IToDoService
    {
        private IToDoRepository toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }

        public ToDo Add(ToDo toDo)
        {
            try
            {
                return this.toDoRepository.Add(toDo);                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ToDo> GetAll()
        {
            try
            {
                return this.toDoRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ToDo Remove(int toDo)
        {
            try
            {
                return this.toDoRepository.Remove(toDo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ToDo Update(ToDo toDo)
        {
            try
            {
                var oldToDo = GetAll().FirstOrDefault(t => t.Id == toDo.Id);
                if (oldToDo == null) return null;

                var today = DateTime.Today;
                if (!oldToDo.Completed && toDo.Completed && toDo.RepetitionType > 0)
                {
                    var nextTask = new ToDo()
                    {
                        Completed = false,
                        Content = toDo.Content,
                        Created = DateTime.Now,
                        Important = toDo.Important,
                        RepetitionType = toDo.RepetitionType
                    };

                    switch (toDo.RepetitionType)
                    {
                        case 1:
                            nextTask.DueDate = DateTime.Now.AddDays(1);
                            break;
                        case 2:
                            nextTask.DueDate = DateTime.Now.AddDays(7);
                            break;
                        case 3:
                            nextTask.DueDate = DateTime.Now.AddMonths(1);
                            break;
                        case 4:
                            nextTask.DueDate = DateTime.Now.AddYears(1);
                            break;
                    }

                    Add(nextTask);
                    Remove(oldToDo.Id);
                }

                return this.toDoRepository.Update(toDo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
