using Day02Problem1.Games;

namespace Day02Problem1
{
    public class Solver
    {
        private readonly IGameFactory _gameFactory;

        public Solver(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private readonly Dictionary<string, int> _possible = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        public long GetSolution(string[] inputLines)
        {
            var games = inputLines.Select(line => _gameFactory.Create(line));
            return games
                .Where(g => g.IsPossibleGivenMaxColorCounts(_possible))
                .Sum(g => g.GameId);
        }
    }
}
