using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private List<Note> notesRepository;

        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;

            this.notesRepository = new List<Note>
            {
                new Note()
                {
                    Title = "First Note",
                    Description = "This is the first note. You can add new notes by using the form."
                }
            };
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return this.notesRepository.ToArray();
        }
    }
}
