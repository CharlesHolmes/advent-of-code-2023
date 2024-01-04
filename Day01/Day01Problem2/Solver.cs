namespace Day01Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            long sum = 0;
            foreach (string line in inputLines)
            {
                sum += new InputLine(line).GetLineValue();
            }

            return sum;
        }
    }
}
