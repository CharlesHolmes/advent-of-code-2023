namespace Day21Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
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

                            string triedKey = $"{nextStep.Location.RowIndex},{nextStep.Location.ColumnIndex},{nextStep.StepsRemaining}";
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
                        if (diff2 - diff1 == diff3 - diff2 
                            && diff3 - diff2 == diff4 - diff3 
                            && start == stepGoal % di)
                        {
                            stepIncrease = di;
                            tileIncreaseDifference = diff2 - diff1;
                            patternFound = true;
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

            return tileRunningTotal;
        }

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
                            RowIndex = i,
                            ColumnIndex = j
                        };
                    }
                }
            }

            throw new InvalidOperationException("Unable to find starting point in input.");
        }

        private static IEnumerable<Coord> GetNextMoves(Coord current, string[] input)
        {
            var potentials = new List<Coord>
            {
                new Coord { RowIndex = current.RowIndex - 1, ColumnIndex = current.ColumnIndex },
                new Coord { RowIndex = current.RowIndex + 1, ColumnIndex = current.ColumnIndex },
                new Coord { RowIndex = current.RowIndex, ColumnIndex = current.ColumnIndex - 1 },
                new Coord { RowIndex = current.RowIndex, ColumnIndex = current.ColumnIndex + 1 }
            };
            return potentials.Where(p =>
            {
                int i = p.RowIndex % input.Length;
                if (i < 0) i += input.Length;
                int j = p.ColumnIndex % input[i].Length;
                if (j < 0) j += input[i].Length;
                return input[i][j] != '#';
            });
        }
    }
}
