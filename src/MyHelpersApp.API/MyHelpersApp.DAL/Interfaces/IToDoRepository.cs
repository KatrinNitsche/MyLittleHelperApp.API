using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Interfaces
{
    public interface IToDoRepository
    {
        IEnumerable<ToDo> GetAll();
        ToDo Add(ToDo toDo);
        ToDo Update(ToDo toDo);
        ToDo Remove(ToDo toDo);
    }
}
