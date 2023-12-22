using System.Diagnostics;

namespace Day22Problem1
{
    public class Point
    {
        public int x;
        public int y;
        public int z;
    }

    public class Brick
    {
        public Point p1;
        public Point p2;
        public string Name;

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

        private static void CompactTheBrickStack(List<Brick> bricks)
        {
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

                    int brickHeight = bricks[i].p2.z - bricks[i].p1.z;
                    bricks[i].p1.z = lowestPossibleHeight;
                    bricks[i].p2.z = bricks[i].p1.z + brickHeight;
                }
            }
        }

        private static int IdentifyRemovalCandidateCount(List<Brick> bricks)
        {
            HashSet<Brick> necessarySupports = new HashSet<Brick>();
            HashSet<Brick> allSupports = new HashSet<Brick>();
            for (int i = 0; i < bricks.Count; i++)
            {
                HashSet<Brick> brickSupporters = new HashSet<Brick>();
                for (int j = 0; j < i; j++)
                {
                    if (bricks[i].IntersectsIfAtSameHeight(bricks[j])
                        && bricks[i].p1.z - 1 == bricks[j].p2.z)
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

            Console.Out.WriteLine("Necessary bricks: " + string.Join(",", necessarySupports.Select(b => b.Name)));
            Console.Out.WriteLine("Bricks that support something: " + string.Join(",", allSupports.Select(b => b.Name)));
            var nonSupports = bricks.Except(allSupports).ToList();
            Console.Out.WriteLine("Bricks with nothing to support: " + string.Join(",", nonSupports.Select(b => b.Name)));
            return nonSupports.Union(bricks.Except(necessarySupports)).Count();
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
            int removalCandidateCount = IdentifyRemovalCandidateCount(bricks);
            Console.Out.WriteLine(removalCandidateCount);
        }
    }
}
