namespace Day02Problem1
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            string[] inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver();
            Console.Out.WriteLine(solver.GetSolution(inputLines));
        }
    }
}