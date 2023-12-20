using System.Collections.Immutable;

namespace Day01Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            long sum = 0;
            foreach (string line in inputLines)
            {
                ImmutableArray<char> digits = line.Where(c => char.IsDigit(c)).ToImmutableArray();
                int firstDigit = digits.First() - '0';
                int lastDigit = digits.Last() - '0';
                int lineSum = firstDigit * 10 + lastDigit;
                sum += lineSum;
            }

            return sum;
        }
    }
}
