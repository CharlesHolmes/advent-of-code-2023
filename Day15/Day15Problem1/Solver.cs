namespace Day15Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            string[] steps = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            long sumOfSteps = 0;
            foreach (string step in steps)
            {
                int stepValue = 0;
                foreach (int charAsciiValue in step)
                {
                    stepValue += charAsciiValue;
                    stepValue *= 17;
                    stepValue %= 256;
                }

                sumOfSteps += stepValue;
            }

            return sumOfSteps;
        }
    }
}
