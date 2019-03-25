namespace SearchToolbox.REST.Classes
{
    public class SearchCriteria
    {
        public string SearchFor { get; set; } = string.Empty;
        public string CodeGreaterThan { get; set; } = string.Empty;
        public int BlockSize { get; set; } = 1000;
    }
}
