using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.BL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {       
        private readonly IToDoService toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }
       
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return this.toDoService.GetAll().OrderBy(x => x.Important).ToArray();
        }

        [HttpPost]
        public ToDo Post(ToDo toDo)
        {
            if (toDo.Id == 0)
            {
                this.toDoService.Add(toDo);
            }
            else
            {
                this.toDoService.Update(toDo);
            }
            
            return toDo;
        }

        [HttpDelete]
        public ToDo Delete(int id)
        {            
            return this.toDoService.Remove(id);
        }
    }
}
