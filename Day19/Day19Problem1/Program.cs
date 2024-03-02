namespace Day19Problem1
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver();
            Console.Out.Write(solver.GetSolution(inputLines));
        }
    }
}
