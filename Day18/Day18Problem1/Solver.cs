namespace Day18Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var dugCoords = new List<Coord>();
            var current = new Coord { RowIndex = 0, ColumnIndex = 0 };
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
                    current = new Coord { RowIndex = current.RowIndex + iDelta, ColumnIndex = current.ColumnIndex + jDelta };
                    iMin = Math.Min(iMin, current.RowIndex);
                    jMin = Math.Min(jMin, current.ColumnIndex);
                    iMax = Math.Max(iMax, current.RowIndex);
                    jMax = Math.Max(jMax, current.ColumnIndex);
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
                map[c.RowIndex - iMin][c.ColumnIndex - jMin] = '#';
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

            return insideCount;
        }
    }
}
