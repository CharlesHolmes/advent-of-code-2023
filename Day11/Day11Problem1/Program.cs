using System.Text;

namespace Day11Problem1
{
    public class Galaxy
    {
        public int GalaxyId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Dictionary<int, int> DistanceToOthers { get; } = new Dictionary<int, int>();
    }

    internal class Program
    {
        private static void GetGalaxyDistances(Dictionary<int, Galaxy> galaxyMap)
        {
            for (int i = 1; i <= galaxyMap.Count; i++)
            {
                for (int j = i + 1; j <= galaxyMap.Count; j++)
                {
                    var galaxy1 = galaxyMap[i];
                    var galaxy2 = galaxyMap[j];
                    // manhattan distance
                    var distance = Math.Abs(galaxy1.Row - galaxy2.Row) + Math.Abs(galaxy1.Column - galaxy2.Column);
                    galaxy1.DistanceToOthers.Add(j, distance);
                }
            }
        }

        private static Dictionary<int, Galaxy> GetGalaxies(string[] inputLines)
        {
            var result = new Dictionary<int, Galaxy>();
            int id = 1;
            for (int i = 0; i < inputLines.Length; i++)
            {
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    if (inputLines[i][j] == '#')
                    {
                        result.Add(id, new Galaxy
                        {
                            GalaxyId = id,
                            Row = i,
                            Column = j
                        });
                        id++;
                    }
                }
            }

            return result;
        }

        private static string[] Transpose(string[] inputLines)
        {
            string[] transposed = new string[inputLines[0].Length];
            for (int j = 0; j < inputLines[0].Length; j++)
            {
                var transposedRow = new StringBuilder();
                for (int i = 0; i < inputLines.Length; i++)
                {
                    transposedRow.Append(inputLines[i][j]);
                }

                transposed[j] = transposedRow.ToString();
            }

            return transposed;
        }

        private static string[] DoubleEmptyColumns(string[] inputLines)
        {
            string[] transposed = Transpose(inputLines);
            string[] doubled = DoubleEmptyRows(transposed);
            return Transpose(doubled);
        }

        private static string[] DoubleEmptyRows(string[] inputLines)
        {
            var result = new List<string>();
            foreach (string s in inputLines)
            {
                if (s.All(c => c == '.'))
                {
                    result.Add(s);
                }

                result.Add(s);
            }

            return result.ToArray();
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            string[] emptyDoubled = DoubleEmptyColumns(DoubleEmptyRows(inputLines));
            Dictionary<int, Galaxy> galaxyMap = GetGalaxies(emptyDoubled);
            GetGalaxyDistances(galaxyMap);
            long distanceSum = 0;
            for (int i = 1; i <= galaxyMap.Count; i++)
            {
                for (int j = i + 1; j <= galaxyMap.Count; j++)
                {
                    distanceSum += galaxyMap[i].DistanceToOthers[j];
                }
            }

            Console.Out.WriteLine(distanceSum);
        }
    }
}
