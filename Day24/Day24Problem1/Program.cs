using System.Diagnostics;

namespace Day24Problem1
{
    public class Hailstone
    {
        public long x0;
        public long y0;
        public long z0; // ignored?

        public int vx;
        public int vy;
        public int vz;

        public bool Intersecting;

        public decimal Slope
        {
            get
            {
                return (decimal)vy / vx;
            }
        }

        public decimal Intercept
        {
            get
            {
                return y0 - (x0 * Slope);
            }
        }

        public bool PathIntersects(Hailstone other, long min, long max)
        {
            if (Slope == other.Slope)
            {
                return Intercept == other.Intercept;
            }

            decimal xInterceptCoord = (other.Intercept - Intercept) / (Slope - other.Slope);
            decimal yInterceptCoord = xInterceptCoord * Slope + Intercept;
            decimal timeOfIntercept = (xInterceptCoord - x0) / vx;
            decimal otherTimeOfIntercept = (xInterceptCoord - other.x0) / other.vx;

            return xInterceptCoord >= min
                && xInterceptCoord <= max
                && yInterceptCoord >= min
                && yInterceptCoord <= max
                && timeOfIntercept >= 0
                && otherTimeOfIntercept >= 0;
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var stones = new List<Hailstone>();
            foreach (string line in inputLines)
            {
                var halves = line.Split('@');
                Debug.Assert(halves.Length == 2);
                var coords = halves[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(coords.Length == 3);
                var velocities = halves[1].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(velocities.Length == 3);
                stones.Add(new Hailstone
                {
                    x0 = long.Parse(coords[0]),
                    y0 = long.Parse(coords[1]),
                    z0 = long.Parse(coords[2]),
                    vx = int.Parse(velocities[0]),
                    vy = int.Parse(velocities[1]),
                    vz = int.Parse(velocities[2])
                });
            }

            int intersections = 0;
            for (int i = 0; i < stones.Count; i++)
            {
                for (int j = i + 1; j < stones.Count; j++)
                {
                    if (stones[i].PathIntersects(stones[j], 200000000000000, 400000000000000))
                    {
                        stones[i].Intersecting = true;
                        stones[j].Intersecting = true;
                        intersections++;
                    }
                }
            }

            Console.Out.WriteLine(intersections);
        }
    }
}
