namespace Day11Problem2
{
    public class Galaxy
    {
        public int GalaxyId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Dictionary<int, long> DistanceToOthers { get; } = new Dictionary<int, long>();
    }
}
