namespace Day18Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var dugPoints = new List<Coord>();
            var current = new Coord { RowIndex = 0, ColumnIndex = 0 };
            long sum = 1; // starting point
            dugPoints.Add(current);
            foreach (string line in inputLines)
            {
                string hexInstructions = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[2];
                string clean = string.Join("", hexInstructions.Where(c => char.IsAsciiLetterOrDigit(c)));
                string key = "RDLU";
                char direction = key[clean.Last() - '0'];
                string hexDistanceString = clean.Substring(0, clean.Length - 1);
                long hexDistance = long.Parse(hexDistanceString, System.Globalization.NumberStyles.HexNumber);
                sum += hexDistance;
                int iDelta = 0, jDelta = 0;
                switch (direction)
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

                current = new Coord { RowIndex = current.RowIndex + (iDelta * hexDistance), ColumnIndex = current.ColumnIndex + (jDelta * hexDistance) };
                dugPoints.Add(current);
            }

            // area of arbitrary polygon - https://en.wikipedia.org/wiki/Shoelace_formula
            long interior = 0;
            for (int i = 1; i <= dugPoints.Count; i++)
            {
                interior += dugPoints[i % dugPoints.Count].ColumnIndex * dugPoints[i - 1].RowIndex;
            }

            for (int i = 1; i <= dugPoints.Count; i++)
            {
                interior -= dugPoints[i % dugPoints.Count].RowIndex * dugPoints[i - 1].ColumnIndex;
            }

            sum += Math.Abs(interior);
            long result = sum / 2 + 1;
            return result;
        }
    }
}
