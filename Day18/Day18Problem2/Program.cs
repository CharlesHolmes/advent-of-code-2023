using System.Diagnostics;

namespace Day18Problem2
{
    public struct Coord
    {
        public long i;
        public long j;
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var dugPoints = new List<Coord>();
            var current = new Coord { i = 0, j = 0 };
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
                //Console.Out.WriteLine($"{clean} - {direction} - {hexDistance}");
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

                current = new Coord { i = current.i + (iDelta * hexDistance), j = current.j + (jDelta * hexDistance) };
                dugPoints.Add(current);
            }

            // area of arbitrary polygon - https://en.wikipedia.org/wiki/Shoelace_formula
            long interior = 0;
            for (int i = 1; i <= dugPoints.Count; i++)
            {
                interior += dugPoints[i % dugPoints.Count].j * dugPoints[i - 1].i;
            }

            for (int i = 1; i <= dugPoints.Count; i++)
            {
                interior -= dugPoints[i % dugPoints.Count].i * dugPoints[i - 1].j;
            }

            sum += Math.Abs(interior);
            Debug.Assert(sum % 2 == 1);
            Console.Out.WriteLine(sum / 2 + 1);
        }
    }
}
