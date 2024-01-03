namespace Day01Problem2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] inputLines = await File.ReadAllLinesAsync(args[0]);
            long sum = 0;
            foreach (string line in inputLines)
            {
                sum += new InputLine(line).GetLineValue();
            }

            Console.Out.WriteLine($"Your overall sum is: {sum}");
        }
    }
}
