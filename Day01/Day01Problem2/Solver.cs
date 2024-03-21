using Day01Problem2.InputLines;

namespace Day01Problem2
{
    public class Solver
    {
        private readonly IInputLineFactory _inputLineFactory;

        public Solver(IInputLineFactory inputLineFactory)
        {
            _inputLineFactory = inputLineFactory;
        }

        public long GetSolution(string[] inputLines)
        {
            long sum = 0;
            foreach (string line in inputLines)
            {
                sum += _inputLineFactory
                    .Create(line)
                    .GetLineValue();
            }

            return sum;
        }
    }
}
