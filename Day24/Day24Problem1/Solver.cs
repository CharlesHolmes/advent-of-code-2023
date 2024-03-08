namespace Day24Problem1
{
    public class Solver
    {
        public int GetSolution(string[] inputLines)
        {
            var stones = new List<Hailstone>();
            foreach (string line in inputLines)
            {
                var halves = line.Split('@');
                if (halves.Length != 2) throw new ArgumentException("Expected input to contain both positions and velocities.");
                var coords = halves[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (coords.Length != 3) throw new ArgumentException("Expected x, y, and z components in each input position.");
                var velocities = halves[1].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (velocities.Length != 3) throw new ArgumentException("Expected vx, vy, and vz components in each input velocity.");
                stones.Add(new Hailstone
                {
                    X0 = long.Parse(coords[0]),
                    Y0 = long.Parse(coords[1]),
                    VX = int.Parse(velocities[0]),
                    VY = int.Parse(velocities[1])
                });
            }

            int intersections = 0;
            for (int i = 0; i < stones.Count; i++)
            {
                for (int j = i + 1; j < stones.Count; j++)
                {
                    if (stones[i].PathIntersects(stones[j], 200000000000000, 400000000000000))
                    {
                        intersections++;
                    }
                }
            }

            return intersections;
        }
    }
}
