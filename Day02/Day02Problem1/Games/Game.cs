using Day02Problem1.Draws;

namespace Day02Problem1.Games
{
    public class Game : IGame
    {
        private readonly List<IDraw> _draws;
        public int GameId { get; private init; }

        public Game(
            IDrawFactory drawFactory,
            string line)
        {
            string trimmed = line.Substring(5);
            GameId = int.Parse(trimmed.Substring(0, trimmed.IndexOf(':')));
            _draws = trimmed.Substring(trimmed.IndexOf(':') + 1).Split(';').Select(s => drawFactory.Create(s)).ToList();
        }

        public bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts)
        {
            foreach (Draw draw in _draws)
            {
                if (!draw.IsPossibleGivenMaxColorCounts(maxColorCounts))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
