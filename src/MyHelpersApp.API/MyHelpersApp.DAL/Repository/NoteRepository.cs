using MyHelpersApp.DAL.Interfaces;
using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Repository
{
    public class NoteRepository : INoteRepository
    {
        private DataContext context;

        public NoteRepository(DataContext context)
        {
            this.context = context;
        }

        public Note Add(Note note)
        {
            context.Notes.Add(note);
            context.SaveChanges();
            return note;
        }

        public IEnumerable<Note> GetAll()
        {
            return context.Notes;
        }

        public Note Remove(Note note)
        {
            context.Remove(note);
            context.SaveChanges();
            return note;
        }

        public Note Update(Note note)
        {
            var entry = context.Notes.Find(note.Id);
            if (entry != null)
            {
                entry.Description = note.Description;
                entry.Title = note.Title;
                context.SaveChanges();
            }

            return entry;
        }
    }
}
