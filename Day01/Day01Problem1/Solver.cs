namespace Day01Problem1
{
    public class Solver
    {
        private readonly ILineFactory _lineFactory;

        public Solver(ILineFactory lineFactory)
        {
            _lineFactory = lineFactory;
        }

        public long GetSolution(string[] inputLines)
        {
            return inputLines
                .Select(input => _lineFactory.Create(input).GetLineValue())
                .Sum();
        }
    }
}
