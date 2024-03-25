using Day02Problem1.DrawnColors;
using Day02Problem1.Draws;
using Day02Problem1.Games;

namespace Day02Problem1
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            string[] inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver(
                new GameFactory(
                    new DrawFactory(
                        new DrawnColorFactory())));
            Console.Out.WriteLine(solver.GetSolution(inputLines));
        }
    }
}