namespace Day02Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var games = inputLines.Select(line => Game.Parse(line));
            return games.Sum(g => g.GetPower());
        }
    }
}
