namespace Day21Problem2
{
    public struct Coord
    {
        public long i;
        public long j;
    }

    public struct Step
    {
        public Coord Location { get; init; }
        public int StepsRemaining { get; init; }
    }

    internal class Program
    {
        private static Coord FindStart(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 'S')
                    {
                        return new Coord
                        {
                            i = i,
                            j = j
                        };
                    }
                }
            }

            throw new Exception("Unable to find start?");
        }

        private static IEnumerable<Coord> GetNextMoves(Coord current, string[] input)
        {
            var potentials = new List<Coord>
            {
                new Coord { i = current.i - 1, j = current.j },
                new Coord { i = current.i + 1, j = current.j },
                new Coord { i = current.i, j = current.j - 1 },
                new Coord { i = current.i, j = current.j + 1 }
            };

            return potentials.Where(p => {
                int i = (int)(p.i % input.Length);
                if (i < 0) i += input.Length;
                int j = (int)(p.j % input[0].Length);
                if (j < 0) j += input[0].Length;
                return input[i][j] != '#';
            });
        }

        private class Result
        {
            public int StartingSteps { get; set; }
            public int ReachedTiles { get; set; }
        }

        static async Task Main(string[] args)
        {
            const int totalSteps = 5000;
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var toExplore = new Queue<Step>();
            Coord start = FindStart(inputLines);
            toExplore.Enqueue(new Step
            {
                Location = start,
                StepsRemaining = totalSteps
            });
            int lastStepsRemaining = totalSteps;
            while (toExplore.Any())
            {
                var current = toExplore.Dequeue();
                if (current.StepsRemaining != lastStepsRemaining)
                {
                    int stepsTaken = totalSteps - current.StepsRemaining;
                    lastStepsRemaining = current.StepsRemaining;
                    Console.Out.WriteLine($"Reached {toExplore.Count} tiles after {stepsTaken} steps.");
                    await File.AppendAllTextAsync("output.csv", $"{stepsTaken},{toExplore.Count}{Environment.NewLine}");
                }

                foreach (Coord nextLocation in GetNextMoves(current.Location, inputLines))
                {
                    var nextStep = new Step
                    {
                        Location = nextLocation,
                        StepsRemaining = current.StepsRemaining - 1
                    };

                    toExplore.Enqueue(nextStep);
                }
            }
        }
    }
}
