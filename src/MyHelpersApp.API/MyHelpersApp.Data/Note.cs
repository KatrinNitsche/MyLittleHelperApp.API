namespace MyHelpersApp.Data
{
    public class Note : BaseDbEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? CategoryId { get; set; }
    }
}
