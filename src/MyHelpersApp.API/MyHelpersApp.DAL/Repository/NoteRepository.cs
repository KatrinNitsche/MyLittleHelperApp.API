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

        public Note Remove(int id)
        {
            var note = context.Notes.Find(id);
            if (note != null)
            {
                context.Remove(note);
                context.SaveChanges();
            }

            return note;
        }

        public Note Update(Note note)
        {
            var entry = context.Notes.Find(note.Id);
            if (entry != null)
            {
                entry.Description = note.Description;
                entry.Title = note.Title;
                entry.ParentId = note.ParentId;
                context.SaveChanges();
            }

            return entry;
        }
    }
}
