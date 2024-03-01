namespace Day17Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var heatLossStats = new HeatLossStats(inputLines);
            // start from top left (using queue)
            var origin = new PathStepKey { Position = new Block { RowIndex = 0, ColumnIndex = 0 }, LastMove = Direction.Right };
            heatLossStats.TotalHeatLoss[origin.Position.RowIndex][origin.Position.ColumnIndex][(int)origin.LastMove][(int)origin.SecondLastMove][(int)origin.ThirdLastMove] = 0;
            var toEvaluate = new Queue<PathStepKey>();
            toEvaluate.Enqueue(origin);
            while (toEvaluate.Any())
            {
                var current = toEvaluate.Dequeue();
                if (current.Position.RowIndex > 0 && current.LastMove != Direction.Down)
                {
                    AttemptExploreAdjacent(current, Direction.Up, toEvaluate, heatLossStats);
                }

                if (current.Position.ColumnIndex > 0 && current.LastMove != Direction.Right)
                {
                    AttemptExploreAdjacent(current, Direction.Left, toEvaluate, heatLossStats);
                }

                if (current.Position.RowIndex < inputLines.Length - 1 && current.LastMove != Direction.Up)
                {
                    AttemptExploreAdjacent(current, Direction.Down, toEvaluate, heatLossStats);
                }

                if (current.Position.ColumnIndex < inputLines[current.Position.RowIndex].Length - 1 && current.LastMove != Direction.Left)
                {
                    AttemptExploreAdjacent(current, Direction.Right, toEvaluate, heatLossStats);
                }
            }

            long result = int.MaxValue;
            for (int i = 0; i < heatLossStats.TotalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1].Length; i++)
            {
                for (int j = 0; j < heatLossStats.TotalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1][i].Length; j++)
                {
                    for (int k = 0; k < heatLossStats.TotalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1][i][j].Length; k++)
                    {
                        result = Math.Min(result, heatLossStats.TotalHeatLoss[inputLines.Length - 1][inputLines[0].Length - 1][i][j][k]);
                    }
                }
            }

            return result;
        }

        private bool IsTooManyInARow(PathStepKey current, Direction nextMove)
        {
            return current.LastMove == nextMove
                && current.SecondLastMove == nextMove
                && current.ThirdLastMove == nextMove;
        }

        private bool BlockAlreadyHasLowerHeatLoss(PathStepKey current, PathStepKey next, HeatLossStats heatLossStats)
        {
            long nextTotal = heatLossStats.TotalHeatLoss[next.Position.RowIndex][next.Position.ColumnIndex][(int)next.LastMove][(int)next.SecondLastMove][(int)next.ThirdLastMove];
            long currentTotal = heatLossStats.TotalHeatLoss[current.Position.RowIndex][current.Position.ColumnIndex][(int)current.LastMove][(int)current.SecondLastMove][(int)current.ThirdLastMove];
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
                LastMove = move,
                SecondLastMove = current.LastMove,
                ThirdLastMove = current.SecondLastMove
            };
        }

        private void AttemptExploreAdjacent(PathStepKey current, Direction move, Queue<PathStepKey> nextSteps, HeatLossStats heatLossStats)
        {
            if (IsTooManyInARow(current, move)) return;
            var nextStep = GetNext(current, move);
            if (BlockAlreadyHasLowerHeatLoss(current, nextStep, heatLossStats)) return;

            long currentTotal = heatLossStats.TotalHeatLoss[current.Position.RowIndex][current.Position.ColumnIndex][(int)current.LastMove][(int)current.SecondLastMove][(int)current.ThirdLastMove];
            heatLossStats.TotalHeatLoss[nextStep.Position.RowIndex][nextStep.Position.ColumnIndex][(int)nextStep.LastMove][(int)nextStep.SecondLastMove][(int)nextStep.ThirdLastMove] = currentTotal + heatLossStats.StepHeatLoss[nextStep.Position.RowIndex][nextStep.Position.ColumnIndex];
            nextSteps.Enqueue(nextStep);
        }
    }
}
