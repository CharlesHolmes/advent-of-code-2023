namespace Day01Problem2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver();
            long solution = solver.GetSolution(inputLines);
            Console.Out.WriteLine($"Your overall sum is: {solution}");
        }
    }
}
