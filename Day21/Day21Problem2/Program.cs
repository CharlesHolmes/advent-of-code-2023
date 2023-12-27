namespace Day21Problem2
{
    public class Coord
    {
        public int i;
        public int j;
    }

    public struct Step
    {
        public Coord Location { get; init; }
        public int StepsRemaining { get; init; }
    }

    internal static class Program
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

            throw new InvalidOperationException("Unable to find start?");
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
            return potentials.Where(p => 
            {
                int i = p.i % input.Length;
                if (i < 0) i += input.Length;
                int j = p.j % input[i].Length;
                if (j < 0) j += input[i].Length;
                return input[i][j] != '#';
            });
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            Coord origin = FindStart(inputLines);
            var reachedBySteps = new Dictionary<int, long>();
            int? stepIncrease = null;
            long? tileIncreaseDifference = null;
            bool patternFound = false;
            const int stepGoal = 26501365;
            int i = 1;
            while (!patternFound)
            {
                var alreadyTried = new Dictionary<string, bool>();
                var toExplore = new Queue<Step>();
                toExplore.Enqueue(new Step
                {
                    Location = origin,
                    StepsRemaining = i
                });
                long tilesReached = 0;
                while (toExplore.Any())
                {
                    var current = toExplore.Dequeue();
                    if (current.StepsRemaining == 0)
                    {
                        tilesReached++;
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

                            string triedKey = $"{nextStep.Location.i},{nextStep.Location.j},{nextStep.StepsRemaining}";
                            if (alreadyTried.TryGetValue(triedKey, out bool tried) && tried)
                            {
                                // pass
                            }
                            else
                            {
                                toExplore.Enqueue(nextStep);
                                alreadyTried[triedKey] = true;
                            }
                        }
                    }
                }

                Console.Out.WriteLine($"Reached {tilesReached} tiles after {i} steps.");
                File.AppendAllLines("output.csv", new[] { $"{i},{tilesReached}" });
                reachedBySteps[i] = tilesReached;
                int minSteps = 1;
                int maxSteps = reachedBySteps.Keys.Max();
                for (int di = 5; di < 200; di++)
                {
                    for (int start = minSteps; start + (di * 4) <= maxSteps; start++)
                    {
                        long t1 = reachedBySteps[start];
                        long t2 = reachedBySteps[start + di];
                        long t3 = reachedBySteps[start + di * 2];
                        long t4 = reachedBySteps[start + di * 3];
                        long t5 = reachedBySteps[start + di * 4];
                        long diff1 = t2 - t1;
                        long diff2 = t3 - t2;
                        long diff3 = t4 - t3;
                        long diff4 = t5 - t4;
                        if (diff2 - diff1 == diff3 - diff2 && diff3 - diff2 == diff4 - diff3)
                        {
                            Console.Out.WriteLine($"Linear diff pattern.  Step start: {start}, stepIncrease: {di}, difference: {diff2 - diff1}");
                            if (start == stepGoal % di)
                            {
                                stepIncrease = di;
                                tileIncreaseDifference = diff2 - diff1;
                                patternFound = true;
                            }
                        }
                    }
                }
                i++;
            }

            if (!stepIncrease.HasValue || !tileIncreaseDifference.HasValue)
            {
                throw new InvalidOperationException($"{nameof(stepIncrease)} and {nameof(tileIncreaseDifference)} cannot be null.");
            }

            int baseSteps = stepGoal % stepIncrease.Value;
            long tileRunningTotal = reachedBySteps[baseSteps];
            long steps = baseSteps;
            long tileIncrement = reachedBySteps[baseSteps + stepIncrease.Value] - reachedBySteps[baseSteps];
            while (steps < stepGoal)
            {
                tileRunningTotal += tileIncrement;
                tileIncrement += tileIncreaseDifference.Value;
                steps += stepIncrease.Value;
            }

            Console.Out.WriteLine($"At step {steps}, tile total: {tileRunningTotal}");
        }
    }
}
