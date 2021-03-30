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

        public IEnumerable<ToDo> GetAll(int? categoryId)
        {
            try
            {
                return this.toDoRepository.GetAll(categoryId);
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
                var oldToDo = GetAll(null).FirstOrDefault(t => t.Id == toDo.Id);
                if (oldToDo == null) return null;

                if (!oldToDo.Completed && toDo.Completed && toDo.RepetitionType > 0)
                {
                    switch (toDo.RepetitionType)
                    {
                        case 1:
                            toDo.DueDate = toDo.DueDate.AddDays(1);
                            break;
                        case 2:
                            toDo.DueDate = toDo.DueDate.AddDays(7);
                            break;
                        case 3:
                            toDo.DueDate = toDo.DueDate.AddMonths(1);
                            break;
                        case 4:
                            toDo.DueDate = toDo.DueDate.AddYears(1);
                            break;
                    }

                    toDo.Completed = false;
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
