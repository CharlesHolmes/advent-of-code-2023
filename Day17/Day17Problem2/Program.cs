using System.Collections.Concurrent;

namespace Day17Problem2
{

    public struct Block
    {
        public int i;
        public int j;
    }

    public struct PathStepKey
    {
        public Block Position;
        public List<Direction> LastMoves;
        public string LastMovesKey
        {
            get
            {
                return string.Join(",", LastMoves);
            }
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    internal class Program
    {
        private static int[][] _stepHeatLoss;
        private static ConcurrentDictionary<string, long>[][] _totalHeatLoss;

        private static bool IsLegalMove(PathStepKey current, Direction nextMove)
        {
            if (nextMove == current.LastMoves.TakeLast(1).Single())
            {
                // no more than 10
                return current.LastMoves.Count < 10 || current.LastMoves.TakeLast(10).Any(move => move != nextMove);
            }
            else
            {
                // we are turning.
                // must be at least 4 of the same move preceding this
                return current.LastMoves.Count >= 4 && current.LastMoves.TakeLast(4).All(move => move == current.LastMoves.TakeLast(1).Single());
            }
        }

        private static bool BlockAlreadyHasLowerHeatLoss(PathStepKey current, PathStepKey next)
        {
            long nextTotal;
            if (_totalHeatLoss[next.Position.i][next.Position.j].TryGetValue(next.LastMovesKey, out long n))
            {
                nextTotal = n;
            }
            else
            {
                nextTotal = long.MaxValue;
            }

            long currentTotal = _totalHeatLoss[current.Position.i][current.Position.j][current.LastMovesKey];
            return nextTotal <= currentTotal + _stepHeatLoss[next.Position.i][next.Position.j];
        }

        private static PathStepKey GetNext(PathStepKey current, Direction move)
        {
            int iDelta = 0, jDelta = 0;
            switch (move)
            {
                case Direction.Up:
                    iDelta = -1;
                    break;
                case Direction.Right:
                    jDelta = 1;
                    break;
                case Direction.Down:
                    iDelta = 1;
                    break;
                case Direction.Left:
                    jDelta = -1;
                    break;
            }

            return new PathStepKey
            {
                Position = new Block { i = current.Position.i + iDelta, j = current.Position.j + jDelta },
                LastMoves = current.LastMoves.Append(move).TakeLast(10).ToList()
            };
        }

        private static void AttemptExploreAdjacent(PathStepKey current, Direction move, Queue<PathStepKey> nextSteps, ref long cellsFilled)
        {
            if (!IsLegalMove(current, move)) return;
            var nextStep = GetNext(current, move);
            if (BlockAlreadyHasLowerHeatLoss(current, nextStep)) return;

            long currentTotal = _totalHeatLoss[current.Position.i][current.Position.j][current.LastMovesKey];
            _totalHeatLoss[nextStep.Position.i][nextStep.Position.j][nextStep.LastMovesKey] = currentTotal + _stepHeatLoss[nextStep.Position.i][nextStep.Position.j];
            Interlocked.Increment(ref cellsFilled);
            nextSteps.Enqueue(nextStep);
        }

        private static void InitHeatLossCollections(string[] inputLines)
        {
            _stepHeatLoss = new int[inputLines.Length][];
            _totalHeatLoss = new ConcurrentDictionary<string, long>[inputLines.Length][];
            Parallel.For(0, inputLines.Length, i =>
            {
                _stepHeatLoss[i] = new int[inputLines[i].Length];
                _totalHeatLoss[i] = new ConcurrentDictionary<string, long>[inputLines[0].Length];
                for (int j = 0; j < inputLines.Length; j++)
                {
                    _stepHeatLoss[i][j] = inputLines[i][j] - '0';
                    _totalHeatLoss[i][j] = new ConcurrentDictionary<string, long>();
                }
            });
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            InitHeatLossCollections(inputLines);
            long totalCells = inputLines.Length * inputLines[0].Length * Enum.GetValues<Direction>().Count() * Enum.GetValues<Direction>().Count() * Enum.GetValues<Direction>().Count();
            long cellsFilled = 0;
            // start from top left (using queue)
            var origin = new PathStepKey { Position = new Block { i = 0, j = 0 }, LastMoves = new List<Direction> { Direction.Right } };
            _totalHeatLoss[origin.Position.i][origin.Position.j][origin.LastMovesKey] = 0;
            var toEvaluate = new Queue<PathStepKey>();
            toEvaluate.Enqueue(origin);
            while (toEvaluate.Any())
            {
                var current = toEvaluate.Dequeue();
                if (current.Position.i > 0 && current.LastMoves.TakeLast(1).Single() != Direction.Down)
                {
                    AttemptExploreAdjacent(current, Direction.Up, toEvaluate, ref cellsFilled);
                }

                if (current.Position.j > 0 && current.LastMoves.TakeLast(1).Single() != Direction.Right)
                {
                    AttemptExploreAdjacent(current, Direction.Left, toEvaluate, ref cellsFilled);
                }

                if (current.Position.i < inputLines.Length - 1 && current.LastMoves.TakeLast(1).Single() != Direction.Up)
                {
                    AttemptExploreAdjacent(current, Direction.Down, toEvaluate, ref cellsFilled);
                }

                if (current.Position.j < inputLines[current.Position.i].Length - 1 && current.LastMoves.TakeLast(1).Single() != Direction.Left)
                {
                    AttemptExploreAdjacent(current, Direction.Right, toEvaluate, ref cellsFilled);
                }

                if (cellsFilled % 1000 == 0)
                {
                    Console.Out.WriteLine($"Filled {cellsFilled} out of {totalCells}.  {(double)cellsFilled / totalCells * 100} percent complete.");
                }
            }

            long result = int.MaxValue;
            foreach (KeyValuePair<string, long> kvp in _totalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1])
            {
                result = Math.Min(result, kvp.Value);
            }

            Console.Out.WriteLine(result);
        }
    }
}
