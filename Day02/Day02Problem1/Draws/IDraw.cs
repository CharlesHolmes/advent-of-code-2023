namespace Day02Problem1.Draws
{
    public interface IDraw
    {
        bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts);
    }
}