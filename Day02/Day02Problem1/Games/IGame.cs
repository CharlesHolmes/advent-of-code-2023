
namespace Day02Problem1.Games
{
    public interface IGame
    {
        int GameId { get; }

        bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts);
    }
}