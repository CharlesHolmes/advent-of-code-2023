namespace Day04Problem2
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver();
            long totalCardCount = solver.GetSolution(inputLines);
            Console.Out.WriteLine(totalCardCount);
        }
    }
}