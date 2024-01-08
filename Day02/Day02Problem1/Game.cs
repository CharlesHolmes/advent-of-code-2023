namespace Day02Problem1
{
    public class Game
    {
        private readonly List<Draw> _draws;
        public int GameId { get; private init; }

        private Game(int gameId, List<Draw> draws)
        {
            GameId = gameId;
            _draws = draws;
        }

        public static Game Parse(string line)
        {
            string trimmed = line.Substring(5);
            string gameIdString = trimmed.Substring(0, trimmed.IndexOf(':'));
            int gameId = int.Parse(gameIdString);
            return new Game(
                gameId,
                trimmed.Substring(trimmed.IndexOf(':') + 1).Split(';').Select(s => Draw.Parse(s)).ToList());
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
