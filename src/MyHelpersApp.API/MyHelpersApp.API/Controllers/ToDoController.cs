using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        //private readonly ILogger<WeatherForecastController> _logger;
        private readonly IToDoRepository toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            //_logger = logger;
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
        public ToDo Delete(ToDo toDo)
        {            
            this.toDoRepository.Remove(toDo);
            return toDo;
        }
    }
}
