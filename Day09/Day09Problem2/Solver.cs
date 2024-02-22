namespace Day09Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            List<Sequence> sequences = inputLines
                .Select(line => new Sequence(line.Split(' ').Select(long.Parse).ToList()))
                .ToList();
            return sequences.Sum(x => x.NextPrediction);
        }
    }
}
