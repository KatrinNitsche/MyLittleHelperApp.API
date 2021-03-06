﻿using MyHelpersApp.Data;
using System.Collections.Generic;

namespace MyHelpersApp.DAL.Interfaces
{
    public interface INoteRepository
    {
        Note Add(Note note);
        IEnumerable<Note> GetAll();
        Note Remove(int note);
        Note Update(Note note);
    }
}
