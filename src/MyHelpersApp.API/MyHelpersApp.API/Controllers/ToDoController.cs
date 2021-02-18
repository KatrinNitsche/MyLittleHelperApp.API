using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {       
        private readonly IToDoRepository toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }
       
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return this.toDoRepository.GetAll().OrderBy(x => x.Important).ToArray();
        }

        [HttpPost]
        public ToDo Post(ToDo toDo)
        {
            this.toDoRepository.Add(toDo);
            return toDo;
        }

        [HttpDelete]
        public ToDo Delete(int id)
        {            
            return this.toDoRepository.Remove(id);
        }
    }
}
