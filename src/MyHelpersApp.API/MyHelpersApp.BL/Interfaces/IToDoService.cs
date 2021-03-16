using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.BL.Interfaces
{
    public interface IToDoService
    {
        IEnumerable<ToDo> GetAll(int? categoryId);
        ToDo Add(ToDo toDo);
        ToDo Update(ToDo toDo);
        ToDo Remove(int toDo);
    }
}
