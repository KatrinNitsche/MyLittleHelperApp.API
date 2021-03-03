using Microsoft.AspNetCore.Mvc;
using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace MyHelpersApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return this.noteRepository.GetAll().OrderBy(x => x.Title).ToArray();
        }

        [HttpPost]
        public Note Post(Note note)
        {
            if (note.Id == 0)
            {
                this.noteRepository.Add(note);
            }
            else
            {
                this.noteRepository.Update(note);
            }

            return note;
        }

        [HttpDelete]
        public Note Delete(int id)
        {
            return this.noteRepository.Remove(id);
        }
    }
}
