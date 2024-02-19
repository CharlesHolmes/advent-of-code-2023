namespace Day07Problem1
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver();
            long totalWinnings = solver.GetSolution(inputLines);
            Console.Out.WriteLine(totalWinnings);
        }
    }
}
