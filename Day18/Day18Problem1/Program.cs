namespace Day18Problem1
{
    public struct Coord
    {
        public int i;
        public int j;
    }

    internal class Program
    {


        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var dugCoords = new List<Coord>();
            var current = new Coord { i = 0, j = 0 };
            int iMin = 0, jMin = 0, iMax = 0, jMax = 0;
            dugCoords.Add(current);
            foreach (string line in inputLines)
            {
                string[] split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int dist = int.Parse(split[1]);
                int iDelta = 0, jDelta = 0;
                switch (split[0][0])
                {
                    case 'R':
                        jDelta = 1;
                        break;
                    case 'D':
                        iDelta = 1;
                        break;
                    case 'L':
                        jDelta = -1;
                        break;
                    case 'U':
                        iDelta = -1;
                        break;
                }

                for (int i = 0; i < dist; i++)
                {
                    current = new Coord { i = current.i + iDelta, j = current.j + jDelta };
                    iMin = Math.Min(iMin, current.i);
                    jMin = Math.Min(jMin, current.j);
                    iMax = Math.Max(iMax, current.i);
                    jMax = Math.Max(jMax, current.j);
                    dugCoords.Add(current);
                }
            }

            var map = new char[iMax - iMin + 1][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = Enumerable.Repeat('.', jMax - jMin + 1).ToArray();
            }

            foreach (Coord c in dugCoords)
            {
                map[c.i - iMin][c.j - jMin] = '#';
            }

            long insideCount = 0;
            for (int i = 0; i < map.Length; i++)
            {
                bool insideFigure = false;
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#' || insideFigure)
                    {
                        insideCount++;
                    }

                    if (i > 0 && map[i][j] == '#' && map[i - 1][j] == '#') insideFigure = !insideFigure;
                }
            }
            
            Console.Out.WriteLine(insideCount);
        }
    }
}
