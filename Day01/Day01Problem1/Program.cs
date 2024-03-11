namespace Day01Problem1
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            string[] inputLines = (await File.ReadAllTextAsync(args[0])).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var solver = new Solver(new LineFactory());
            long solution = solver.GetSolution(inputLines);
            Console.Out.WriteLine($"Your overall sum is: {solution}");
        }
    }
}
