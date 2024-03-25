namespace Day02Problem1.DrawnColors
{
    public class DrawnColor : IDrawnColor
    {
        public string CubeColor { get; private init; } = string.Empty;
        public int CubeCount { get; private init; }

        public DrawnColor(string drawnColorString)
        {
            string[] parts = drawnColorString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            CubeColor = parts[1];
            CubeCount = int.Parse(parts[0]);
        }

        public bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts)
        {
            return maxColorCounts.ContainsKey(CubeColor) && maxColorCounts[CubeColor] >= CubeCount;
        }
    }
}
