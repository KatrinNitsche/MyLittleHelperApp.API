﻿using System;

namespace MyHelpersApp.Data
{
    public class ToDo : BaseDbEntry
    {
        public string Content { get; set; }
        public bool Completed { get; set; }
        public bool Important { get; set; }
        public int RepetitionType { get; set; }
        public int? RepetitionOfId { get; set; }
        public DateTime DueDate { get; set; }
        public int? CategoryId { get; set; }
    }   
}
