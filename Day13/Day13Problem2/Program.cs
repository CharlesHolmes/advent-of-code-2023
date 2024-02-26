namespace Day13Problem2
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var solver = new Solver();
            Console.WriteLine(solver.GetSolution(inputLines));
        }
    }
}
