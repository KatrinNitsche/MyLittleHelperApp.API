﻿using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private DataContext context;

        public ToDoRepository(DataContext context)
        {
            this.context = context;
        }

        public ToDo Add(ToDo toDo)
        {
            toDo.Created = DateTime.Now;
            toDo.Updated = null;
            context.ToDos.Add(toDo);
            context.SaveChanges();
            return toDo;
        }

        public ToDo Update(ToDo toDo)
        {
            var entry = context.ToDos.Find(toDo.Id);
            if (entry != null)
            {
                entry.Content = toDo.Content;
                entry.Completed = toDo.Completed;
                entry.Important = toDo.Important;
                entry.Updated = DateTime.Now;
                entry.DueDate = toDo.DueDate;
                entry.RepetitionType = toDo.RepetitionType;
                context.SaveChanges();
            }

            return entry;
        }

        public IEnumerable<ToDo> GetAll()
        {
            return context.ToDos;
        }

        public ToDo Remove(int id)
        {
            var toDo = context.ToDos.Find(id);
            if (toDo != null)
            {
                context.Remove(toDo);
                context.SaveChanges();
            }

            return toDo;
        }
    }
}
