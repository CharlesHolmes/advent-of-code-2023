namespace Day17Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var heatLossStats = new HeatLossStats(inputLines);
            // start from top left (using queue)
            var origin = new PathStepKey { Position = new Block { RowIndex = 0, ColumnIndex = 0 }, LastMoves = new List<Direction> { Direction.Right } };
            heatLossStats.TotalHeatLoss[origin.Position.RowIndex][origin.Position.ColumnIndex][origin.LastMovesKey] = 0;
            var toEvaluate = new PriorityQueue<PathStepKey, long>();
            toEvaluate.Enqueue(origin, 0);
            while (toEvaluate.TryDequeue(out PathStepKey current, out long currentHeatLoss))
            {
                if (current.Position.RowIndex == inputLines.Length - 1
                    && current.Position.ColumnIndex == inputLines.Last().Length - 1)
                {
                    return currentHeatLoss;
                }

                if (current.Position.RowIndex > 0 && current.LastMoves.TakeLast(1).Single() != Direction.Down)
                {
                    AttemptExploreAdjacent(current, Direction.Up, toEvaluate, heatLossStats);
                }

                if (current.Position.ColumnIndex > 0 && current.LastMoves.TakeLast(1).Single() != Direction.Right)
                {
                    AttemptExploreAdjacent(current, Direction.Left, toEvaluate, heatLossStats);
                }

                if (current.Position.RowIndex < inputLines.Length - 1 && current.LastMoves.TakeLast(1).Single() != Direction.Up)
                {
                    AttemptExploreAdjacent(current, Direction.Down, toEvaluate, heatLossStats);
                }

                if (current.Position.ColumnIndex < inputLines[current.Position.RowIndex].Length - 1 && current.LastMoves.TakeLast(1).Single() != Direction.Left)
                {
                    AttemptExploreAdjacent(current, Direction.Right, toEvaluate, heatLossStats);
                }
            }

            throw new InvalidOperationException("Unable to reach goal position.");
        }
        
        private bool IsLegalMove(PathStepKey current, Direction nextMove)
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

        private bool BlockAlreadyHasLowerHeatLoss(PathStepKey current, PathStepKey next, HeatLossStats heatLossStats)
        {
            long nextTotal;
            if (heatLossStats.TotalHeatLoss[next.Position.RowIndex][next.Position.ColumnIndex].TryGetValue(next.LastMovesKey, out long n))
            {
                nextTotal = n;
            }
            else
            {
                nextTotal = long.MaxValue;
            }

            long currentTotal = heatLossStats.TotalHeatLoss[current.Position.RowIndex][current.Position.ColumnIndex][current.LastMovesKey];
            return nextTotal <= currentTotal + heatLossStats.StepHeatLoss[next.Position.RowIndex][next.Position.ColumnIndex];
        }

        private PathStepKey GetNext(PathStepKey current, Direction move)
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
                Position = new Block { RowIndex = current.Position.RowIndex + iDelta, ColumnIndex = current.Position.ColumnIndex + jDelta },
                LastMoves = current.LastMoves.Append(move).TakeLast(10).ToList()
            };
        }

        private void AttemptExploreAdjacent(PathStepKey current, Direction move, PriorityQueue<PathStepKey, long> nextSteps, HeatLossStats heatLossStats)
        {
            if (!IsLegalMove(current, move)) return;
            var nextStep = GetNext(current, move);
            if (BlockAlreadyHasLowerHeatLoss(current, nextStep, heatLossStats)) return;

            long currentTotal = heatLossStats.TotalHeatLoss[current.Position.RowIndex][current.Position.ColumnIndex][current.LastMovesKey];
            long nextHeatLoss = currentTotal + heatLossStats.StepHeatLoss[nextStep.Position.RowIndex][nextStep.Position.ColumnIndex];
            heatLossStats.TotalHeatLoss[nextStep.Position.RowIndex][nextStep.Position.ColumnIndex][nextStep.LastMovesKey] = nextHeatLoss;
            nextSteps.Enqueue(nextStep, nextHeatLoss);
        }
    }
}
