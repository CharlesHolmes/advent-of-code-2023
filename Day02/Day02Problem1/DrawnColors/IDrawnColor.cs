namespace Day02Problem1.DrawnColors
{
    public interface IDrawnColor
    {
        string CubeColor { get; }
        int CubeCount { get; }

        bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts);
    }
}