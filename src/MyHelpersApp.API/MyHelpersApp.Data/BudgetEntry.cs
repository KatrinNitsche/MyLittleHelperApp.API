using System;

namespace MyHelpersApp.Data
{
    public class BudgetEntry : BaseDbEntry
    {
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime BudgetDate { get; set; }
        public int RepetitionType { get; set; }
        public int? RepitionOfId { get; set; }
    }
}
