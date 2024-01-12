namespace Day03Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var grid = Grid.ParseInput(inputLines);
            return grid.PartNumbers
                .Where(pn => pn.HasAdjacentSymbol)
                .Select(pn => pn.Number)
                .Sum();
        }
    }
}
