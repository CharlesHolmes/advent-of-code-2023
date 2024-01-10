namespace Day02Problem2
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

        public long GetPower()
        {
            var colorMinimums = new Dictionary<string, long>();
            foreach (Draw draw in _draws)
            {
                foreach (DrawnColor color in draw.DrawnColors)
                {
                    if (!colorMinimums.ContainsKey(color.CubeColor))
                    {
                        colorMinimums.Add(color.CubeColor, color.CubeCount);
                    }
                    else
                    {
                        colorMinimums[color.CubeColor] = Math.Max(colorMinimums[color.CubeColor], color.CubeCount);
                    }
                }
            }

            return colorMinimums.Values.Aggregate(1L, (x, y) => x * y);
        }
    }
}
