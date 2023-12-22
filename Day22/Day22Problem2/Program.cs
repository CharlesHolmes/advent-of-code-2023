using System.Diagnostics;

namespace Day22Problem2
{

    public class Point
    {
        public int x;
        public int y;
        public int z;

        public Point()
        {

        }

        public Point(Point other)
        {
            x = other.x;
            y = other.y;
            z = other.z;
        }
    }

    public class Brick
    {
        public Point p1;
        public Point p2;
        public string Name;

        public Brick()
        {

        }

        public Brick(Brick other)
        {
            p1 = new Point(other.p1);
            p2 = new Point(other.p2);
        }

        public override string ToString()
        {
            return $"{Name}: {p1.x},{p1.y},{p1.z}~{p2.x},{p2.y},{p2.z}";
        }

        public bool IntersectsIfAtSameHeight(Brick other)
        {
            return Math.Min(p1.x, p2.x) <= Math.Max(other.p1.x, other.p2.x)
                && Math.Max(p1.x, p2.x) >= Math.Min(other.p1.x, other.p2.x)
                && Math.Max(p1.y, p2.y) >= Math.Min(other.p1.y, other.p2.y)
                && Math.Min(p1.y, p2.y) <= Math.Max(other.p1.y, other.p2.y);
        }
    }

    internal class Program
    {
        private static Point ParsePoint(string s)
        {
            string[] parts = s.Split(',');
            Debug.Assert(parts.Length == 3);
            return new Point
            {
                x = int.Parse(parts[0]),
                y = int.Parse(parts[1]),
                z = int.Parse(parts[2])
            };
        }

        private static int CompactTheBrickStack(List<Brick> bricks)
        {
            int fallenBrickCount = 0;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].p1.z > 1)
                {
                    int lowestPossibleHeight = 1;
                    // find whatever below
                    for (int j = 0; j < i; j++)
                    {
                        if (bricks[i].IntersectsIfAtSameHeight(bricks[j]))
                        {
                            lowestPossibleHeight = Math.Max(lowestPossibleHeight, bricks[j].p2.z + 1);
                        }
                    }

                    if (lowestPossibleHeight < bricks[i].p1.z)
                    {
                        int brickHeight = bricks[i].p2.z - bricks[i].p1.z;
                        bricks[i].p1.z = lowestPossibleHeight;
                        bricks[i].p2.z = bricks[i].p1.z + brickHeight;
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
            foreach (Brick brick in bricks)
            {
                allDisintegrations += HowManyMoveIfDisintegrated(bricks, brick);
            }

            return allDisintegrations;
        }

        static async Task Main(string[] args)
        {
            string[] inputLines = await File.ReadAllLinesAsync(args[0]);
            var bricks = new List<Brick>();
            int name = 1;
            foreach (string line in inputLines)
            {
                string[] halves = line.Split('~');
                Debug.Assert(halves.Length == 2);
                var firstPoint = ParsePoint(halves[0]);
                var secondPoint = ParsePoint(halves[1]);
                if (firstPoint.z < secondPoint.z)
                {
                    bricks.Add(new Brick
                    {
                        p1 = firstPoint,
                        p2 = secondPoint,
                        Name = name.ToString()
                    });
                }
                else
                {
                    bricks.Add(new Brick
                    {
                        p1 = secondPoint,
                        p2 = firstPoint,
                        Name = name.ToString()
                    });
                }

                name++;
            }

            bricks = bricks.OrderBy(b => b.p1.z).ToList();
            CompactTheBrickStack(bricks);
            bricks = bricks.OrderBy(b => b.p1.z).ToList();
            long disintegrationSum = GetDisintegrationSum(bricks);
            Console.Out.WriteLine(disintegrationSum);
        }
    }
}
