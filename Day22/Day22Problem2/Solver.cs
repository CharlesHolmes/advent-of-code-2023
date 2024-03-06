namespace Day22Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var bricks = new List<Brick>();
            foreach (string line in inputLines)
            {
                string[] halves = line.Split('~');
                if (halves.Length != 2) throw new ArgumentException("Expected two halves in each line of input.");
                var firstPoint = ParsePoint(halves[0]);
                var secondPoint = ParsePoint(halves[1]);
                if (firstPoint.Z < secondPoint.Z)
                {
                    bricks.Add(new Brick(firstPoint, secondPoint));
                }
                else
                {
                    bricks.Add(new Brick(secondPoint, firstPoint));
                }
            }

            bricks = bricks.OrderBy(b => b.Point1.Z).ToList();
            CompactTheBrickStack(bricks);
            bricks = bricks.OrderBy(b => b.Point1.Z).ToList();
            long disintegrationSum = GetDisintegrationSum(bricks);
            return disintegrationSum;
        }

        private static Point ParsePoint(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3) throw new ArgumentException("Expected three components in each point");
            return new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
        }

        private static int CompactTheBrickStack(List<Brick> bricks)
        {
            int fallenBrickCount = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Point1.Z > 1)
                {
                    int lowestPossibleHeight = 1;
                    // find whatever below
                    for (int j = 0; j < i; j++)
                    {
                        if (bricks[i].IntersectsIfAtSameHeight(bricks[j]))
                        {
                            lowestPossibleHeight = Math.Max(lowestPossibleHeight, bricks[j].Point2.Z + 1);
                        }
                    }

                    if (lowestPossibleHeight < bricks[i].Point1.Z)
                    {
                        int brickHeight = bricks[i].Point2.Z - bricks[i].Point1.Z;
                        bricks[i].Point1.Z = lowestPossibleHeight;
                        bricks[i].Point2.Z = bricks[i].Point1.Z + brickHeight;
                        fallenBrickCount++;
                    }
                }
            }

            return fallenBrickCount;
        }

        private static int HowManyMoveIfDisintegrated(List<Brick> bricks, Brick toDisintegrate)
        {
            var disintegrated = bricks.Except(new List<Brick> { toDisintegrate })
                .Select(b => new Brick(b))
                .ToList();
            return CompactTheBrickStack(disintegrated);
        }

        private static long GetDisintegrationSum(List<Brick> bricks)
        {
            long allDisintegrations = 0;
            Parallel.ForEach(bricks, brick =>
            {
                Interlocked.Add(ref allDisintegrations, HowManyMoveIfDisintegrated(bricks, brick));
            });

            return allDisintegrations;
        }
    }
}
