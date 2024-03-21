using Day01Problem2.Digits;
using Day01Problem2.InputLines;
using Day01Problem2.NumberWords;
using System.Diagnostics.CodeAnalysis;

namespace Day01Problem2
{
    internal static class Program
    {
        [ExcludeFromCodeCoverage]
        static async Task Main(string[] args)
        {
            string[] inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver(
                new InputLineFactory(
                    new NumberWordSearchFactory(), new DigitSearchFactory()));
            long solution = solver.GetSolution(inputLines);
            Console.Out.WriteLine($"Your overall sum is: {solution}");
        }
    }
}
