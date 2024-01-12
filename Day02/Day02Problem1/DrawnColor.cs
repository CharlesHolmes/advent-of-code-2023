namespace Day02Problem1
{
    public class DrawnColor
    {
        public string CubeColor { get; private init; } = string.Empty;
        public int CubeCount { get; private init; }

        private DrawnColor()
        {
        }

        public static DrawnColor Parse(string drawnColorString)
        {
            string[] parts = drawnColorString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new DrawnColor
            {
                CubeColor = parts[1],
                CubeCount = int.Parse(parts[0])
            };
        }

        public bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts)
        {
            return maxColorCounts.ContainsKey(CubeColor) && maxColorCounts[CubeColor] >= CubeCount;
        }
    }
}
