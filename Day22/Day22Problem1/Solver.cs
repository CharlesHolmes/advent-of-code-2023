namespace Day22Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var bricks = new List<Brick>();
            foreach (string line in inputLines)
            {
                string[] halves = line.Split('~');
                if (halves.Length != 2) throw new ArgumentException("Expected two halves in each input line");
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
            long removalCandidateCount = IdentifyRemovalCandidateCount(bricks);
            return removalCandidateCount;
        }

        private static Point ParsePoint(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3) throw new ArgumentException("Expected three parts in each point");
            return new Point
            {
                X = int.Parse(parts[0]),
                Y = int.Parse(parts[1]),
                Z = int.Parse(parts[2])
            };
        }

        private static void CompactTheBrickStack(List<Brick> bricks)
        {
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

                    int brickHeight = bricks[i].Point2.Z - bricks[i].Point1.Z;
                    bricks[i].Point1.Z = lowestPossibleHeight;
                    bricks[i].Point2.Z = bricks[i].Point1.Z + brickHeight;
                }
            }
        }

        private static long IdentifyRemovalCandidateCount(List<Brick> bricks)
        {
            HashSet<Brick> necessarySupports = new HashSet<Brick>();
            HashSet<Brick> allSupports = new HashSet<Brick>();
            for (int i = 0; i < bricks.Count; i++)
            {
                HashSet<Brick> brickSupporters = new HashSet<Brick>();
                for (int j = 0; j < i; j++)
                {
                    if (bricks[i].IntersectsIfAtSameHeight(bricks[j])
                        && bricks[i].Point1.Z - 1 == bricks[j].Point2.Z)
                    {
                        brickSupporters.Add(bricks[j]);
                        allSupports.Add(bricks[j]);
                    }
                }

                if (brickSupporters.Count == 1)
                {
                    foreach (Brick supporter in brickSupporters)
                    {
                        necessarySupports.Add(supporter);
                    }
                }
            }

            var nonSupports = bricks.Except(allSupports).ToList();
            return nonSupports.Union(bricks.Except(necessarySupports)).Count();
        }
    }
}
