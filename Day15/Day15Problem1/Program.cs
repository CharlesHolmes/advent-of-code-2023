using System.Diagnostics;

namespace Day15Problem1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            Debug.Assert(inputLines.Length == 1);
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

            Console.Out.WriteLine(sumOfSteps);
        }
    }
}
