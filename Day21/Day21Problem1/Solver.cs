namespace Day21Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            const int startingSteps = 64;
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

            return reached.Count;
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
            var potentials = new List<Coord>();
            if (current.RowIndex > 0) potentials.Add(new Coord { RowIndex = current.RowIndex - 1, ColumnIndex = current.ColumnIndex });
            if (current.RowIndex < input.Length - 1) potentials.Add(new Coord { RowIndex = current.RowIndex + 1, ColumnIndex = current.ColumnIndex });
            if (current.ColumnIndex > 0) potentials.Add(new Coord { RowIndex = current.RowIndex, ColumnIndex = current.ColumnIndex - 1 });
            if (current.ColumnIndex < input[0].Length - 1) potentials.Add(new Coord { RowIndex = current.RowIndex, ColumnIndex = current.ColumnIndex + 1 });
            return potentials.Where(p => input[p.RowIndex][p.ColumnIndex] != '#');
        }
    }
}
