namespace Day21Problem1
{
    public struct Coord
    {
        public int i;
        public int j;
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
            var potentials = new List<Coord>();
            if (current.i > 0) potentials.Add(new Coord { i = current.i - 1, j = current.j });
            if (current.i < input.Length - 1) potentials.Add(new Coord { i = current.i + 1, j = current.j });
            if (current.j > 0) potentials.Add(new Coord { i = current.i, j = current.j - 1 });
            if (current.j < input[0].Length - 1) potentials.Add(new Coord { i = current.i, j = current.j + 1 });
            return potentials.Where(p => input[p.i][p.j] != '#');
        }

        static async Task Main(string[] args)
        {
            const int startingSteps = 64;
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var reached = new HashSet<Coord>();
            var alreadyTriedWithRemaining = new Dictionary<Coord, bool[]>();
            var toExplore = new Queue<Step>();
            toExplore.Enqueue(new Step
            {
                Location = FindStart(inputLines),
                StepsRemaining = startingSteps
            });
            while (toExplore.Any())
            {
                var current = toExplore.Dequeue();
                if (current.StepsRemaining == 0)
                {
                    reached.Add(current.Location);
                }
                else
                {
                    foreach (Coord nextLocation in GetNextMoves(current.Location, inputLines))
                    {
                        var nextStep = new Step
                        {
                            Location = nextLocation,
                            StepsRemaining = current.StepsRemaining - 1
                        };

                        if (!alreadyTriedWithRemaining.ContainsKey(nextStep.Location))
                        {
                            alreadyTriedWithRemaining[nextStep.Location] = new bool[startingSteps];
                        }

                        if (!alreadyTriedWithRemaining[nextLocation][nextStep.StepsRemaining])
                        {
                            toExplore.Enqueue(nextStep);
                            alreadyTriedWithRemaining[nextStep.Location][nextStep.StepsRemaining] = true;
                        }
                    }
                }
            }

            Console.Out.WriteLine(reached.Count);
        }
    }
}
