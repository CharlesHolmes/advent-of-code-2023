using System.Numerics;
using System.Text;

namespace Day11Problem2
{
    public class Galaxy
    {
        public int GalaxyId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Dictionary<int, BigInteger> DistanceToOthers { get; } = new Dictionary<int, BigInteger>();
    }

    internal class Program
    {
        private static Func<int, int, bool> GetComparerForSign(int sign)
        {
            if (sign > 0)
            {
                return (a, b) => a < b;
            }
            else
            {
                return (a, b) => a > b;
            }
        }

        private static void GetGalaxyDistances(Dictionary<int, Galaxy> galaxyMap, bool[] emptyRows, bool[] emptyColumns)
        {
            for (int i = 1; i <= galaxyMap.Count; i++)
            {
                for (int j = i + 1; j <= galaxyMap.Count; j++)
                {
                    var galaxy1 = galaxyMap[i];
                    var galaxy2 = galaxyMap[j];
                    // manhattan distance
                    BigInteger distance = 0;
                    if (galaxy1.Row != galaxy2.Row)
                    {
                        int increment = Math.Sign(galaxy2.Row - galaxy1.Row);
                        Func<int, int, bool> comparer = GetComparerForSign(increment);
                        for (int row = galaxy1.Row; comparer(row, galaxy2.Row) ; row += increment)
                        {
                            if (emptyRows[row])
                            {
                                distance += 1000000;
                            }
                            else
                            {
                                distance += 1;
                            }
                        }
                    }
                    if (galaxy1.Column != galaxy2.Column)
                    {
                        int increment = Math.Sign(galaxy2.Column - galaxy1.Column);
                        Func<int, int, bool> comparer = GetComparerForSign(increment);
                        for (int col = galaxy1.Column; comparer(col, galaxy2.Column); col += increment)
                        {
                            if (emptyColumns[col])
                            {
                                distance += 1000000;
                            }
                            else
                            {
                                distance += 1;
                            }
                        }
                    }

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

        private static bool[] GetEmptyColumnIndexes(string[] inputLines)
        {
            string[] transposed = Transpose(inputLines);
            return GetEmptyRowIndexes(transposed);
        }

        private static bool[] GetEmptyRowIndexes(string[] inputLines)
        {
            bool[] result = new bool[inputLines.Length];
            for (int i = 0; i < inputLines.Length; i++)
            {
                if (inputLines[i].All(c => c == '.'))
                {
                    result[i] = true;
                }
            }

            return result;
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            bool[] emptyRowIndexes = GetEmptyRowIndexes(inputLines);
            bool[] emptyColumnIndexes = GetEmptyColumnIndexes(inputLines);
            Dictionary<int, Galaxy> galaxyMap = GetGalaxies(inputLines);
            GetGalaxyDistances(galaxyMap, emptyRowIndexes, emptyColumnIndexes);
            BigInteger distanceSum = 0;
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
