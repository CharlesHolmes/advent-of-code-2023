namespace Day03Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var grid = Grid.Parse(inputLines);
            return grid.Symbols
                .Where(x => x.IsGear())
                .Sum(x => x.GetGearRatio());
        }
    }
}
