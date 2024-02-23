namespace Day11Problem1
{
    public class Galaxy
    {
        public int GalaxyId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Dictionary<int, int> DistanceToOthers { get; } = new Dictionary<int, int>();
    }
}
