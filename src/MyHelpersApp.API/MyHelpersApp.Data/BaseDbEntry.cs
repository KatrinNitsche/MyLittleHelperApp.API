using System;

namespace MyHelpersApp.Data
{
    public class BaseDbEntry
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
