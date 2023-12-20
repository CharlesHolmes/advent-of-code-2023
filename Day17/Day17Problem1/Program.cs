
namespace Day17Problem1
{
    public struct Block
    {
        public int i;
        public int j;
    }

    public struct PathStepKey
    {
        public Block Position;
        public Direction LastMove;
        public Direction SecondLastMove;
        public Direction ThirdLastMove;
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
        private static long[][][][][] _totalHeatLoss;

        private static bool IsTooManyInARow(PathStepKey current, Direction nextMove)
        {
            return current.LastMove == nextMove
                && current.SecondLastMove == nextMove
                && current.ThirdLastMove == nextMove;
        }

        private static bool BlockAlreadyHasLowerHeatLoss(PathStepKey current, PathStepKey next)
        {
            long nextTotal = _totalHeatLoss[next.Position.i][next.Position.j][(int)next.LastMove][(int)next.SecondLastMove][(int)next.ThirdLastMove];
            long currentTotal = _totalHeatLoss[current.Position.i][current.Position.j][(int)current.LastMove][(int)current.SecondLastMove][(int)current.ThirdLastMove];
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
                LastMove = move,
                SecondLastMove = current.LastMove,
                ThirdLastMove = current.SecondLastMove
            };
        }

        private static void AttemptExploreAdjacent(PathStepKey current, Direction move, Queue<PathStepKey> nextSteps, ref long cellsFilled)
        {
            if (IsTooManyInARow(current, move)) return;
            var nextStep = GetNext(current, move);
            if (BlockAlreadyHasLowerHeatLoss(current, nextStep)) return;

            long currentTotal = _totalHeatLoss[current.Position.i][current.Position.j][(int)current.LastMove][(int)current.SecondLastMove][(int)current.ThirdLastMove];
            _totalHeatLoss[nextStep.Position.i][nextStep.Position.j][(int)nextStep.LastMove][(int)nextStep.SecondLastMove][(int)nextStep.ThirdLastMove] = currentTotal + _stepHeatLoss[nextStep.Position.i][nextStep.Position.j];
            Interlocked.Increment(ref cellsFilled);
            nextSteps.Enqueue(nextStep);
        }

        private static void InitHeatLossCollections(string[] inputLines)
        {
            _stepHeatLoss = new int[inputLines.Length][];
            _totalHeatLoss = new long[inputLines.Length][][][][];
            Parallel.For(0, inputLines.Length, i =>
            {
                _stepHeatLoss[i] = new int[inputLines[i].Length];
                _totalHeatLoss[i] = new long[inputLines[i].Length][][][];
                for (int j = 0; j < inputLines.Length; j++)
                {
                    _stepHeatLoss[i][j] = inputLines[i][j] - '0';
                    _totalHeatLoss[i][j] = new long[Enum.GetValues<Direction>().Count()][][];
                    foreach (Direction d1 in Enum.GetValues<Direction>())
                    {
                        _totalHeatLoss[i][j][(int)d1] = new long[Enum.GetValues<Direction>().Count()][];
                        foreach (Direction d2 in Enum.GetValues<Direction>())
                        {
                            _totalHeatLoss[i][j][(int)d1][(int)d2] = new long[Enum.GetValues<Direction>().Count()];
                            foreach (Direction d3 in Enum.GetValues<Direction>())
                            {
                                _totalHeatLoss[i][j][(int)d1][(int)d2][(int)d3] = long.MaxValue;
                            }
                        }
                    }
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
            var origin = new PathStepKey { Position = new Block { i = 0, j = 0 }, LastMove = Direction.Right };
            _totalHeatLoss[origin.Position.i][origin.Position.j][(int)origin.LastMove][(int)origin.SecondLastMove][(int)origin.ThirdLastMove] = 0;
            var toEvaluate = new Queue<PathStepKey>();
            toEvaluate.Enqueue(origin);
            while (toEvaluate.Any())
            {
                var current = toEvaluate.Dequeue();
                if (current.Position.i > 0 && current.LastMove != Direction.Down)
                {
                    AttemptExploreAdjacent(current, Direction.Up, toEvaluate, ref cellsFilled);
                }

                if (current.Position.j > 0 && current.LastMove != Direction.Right)
                {
                    AttemptExploreAdjacent(current, Direction.Left, toEvaluate, ref cellsFilled);
                }

                if (current.Position.i < inputLines.Length - 1 && current.LastMove != Direction.Up)
                {
                    AttemptExploreAdjacent(current, Direction.Down, toEvaluate, ref cellsFilled);
                }

                if (current.Position.j < inputLines[current.Position.i].Length - 1 && current.LastMove != Direction.Left)
                {
                    AttemptExploreAdjacent(current, Direction.Right, toEvaluate, ref cellsFilled);
                }

                if (cellsFilled % 1000 == 0)
                {
                    Console.Out.WriteLine($"Filled {cellsFilled} out of {totalCells}.  {(double)cellsFilled / totalCells * 100} percent complete.");
                }
            }

            long result = int.MaxValue;
            for (int i = 0; i < _totalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1].Length; i++)
            {
                for (int j = 0; j < _totalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1][i].Length; j++)
                {
                    for (int k = 0; k < _totalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1][i][j].Length; k++) 
                    {
                        result = Math.Min(result, _totalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1][i][j][k]);
                    }
                }
            }

            Console.Out.WriteLine(result);
        }
    }
}