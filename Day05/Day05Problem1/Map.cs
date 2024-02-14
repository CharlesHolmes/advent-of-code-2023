namespace Day05Problem1
{
    public partial class Solver
    {
        public class Map
        {
            public string SourceName { get; set; } = string.Empty;
            public string DestName { get; set; } = string.Empty;
            public List<SourceDestRange> Ranges { get; } = new List<SourceDestRange>();
        }
    }
}
