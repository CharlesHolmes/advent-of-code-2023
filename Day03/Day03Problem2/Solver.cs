namespace Day03Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            // search for asterisks
            var asteriskList = FindAsterisks(inputLines);

            // for each asterisk, search around it for numbers
            foreach (Asterisk asterisk in asteriskList)
            {
                PopulateNeighboringNumbers(asterisk, inputLines);
            }

            // if there are exactly two numbers, multiply them and add to total
            return asteriskList
                .Where(x => x.NeighboringNumbers.Count == 2)
                .Select(x => x.NeighboringNumbers.Aggregate(1L, (x, y) => x * y))
                .Sum();
        }

        private static List<Asterisk> FindAsterisks(string[] inputLines)
        {
            var asteriskList = new List<Asterisk>();
            for (int i = 0; i < inputLines.Length; i++)
            {
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    if (inputLines[i][j] == '*') asteriskList.Add(new Asterisk { i = i, j = j });
                }
            }

            return asteriskList;
        }

        private static void PopulateNeighboringNumbers(Asterisk asterisk, string[] inputLines)
        {
            // find surrounding numbers
            var visited = new HashSet<Coord>();
            for (int idelta = -1; idelta <= 1; idelta++)
            {
                for (int jdelta = -1; jdelta <= 1; jdelta++)
                {
                    if (asterisk.i + idelta < 0) continue;
                    else if (asterisk.j + jdelta < 0) continue;
                    else if (asterisk.i + idelta >= inputLines.Length) continue;
                    else if (asterisk.j + jdelta >= inputLines[asterisk.i + idelta].Length) continue;

                    var coord = new Coord { i = asterisk.i + idelta, j = asterisk.j + jdelta };
                    if (visited.Contains(coord)) continue;
                    visited.Add(coord);
                    char c = inputLines[coord.i][coord.j];
                    if (char.IsDigit(c))
                    {
                        var numberDigits = new List<int>();
                        numberDigits.Add(c - '0');
                        int toLeft = coord.j;
                        while (toLeft - 1 >= 0 && char.IsDigit(inputLines[coord.i][toLeft - 1]))
                        {
                            toLeft--;
                            var leftCoord = new Coord { i = coord.i, j = toLeft };
                            visited.Add(leftCoord);
                            numberDigits.Insert(0, inputLines[leftCoord.i][leftCoord.j] - '0');
                        }

                        int toRight = coord.j;
                        while (toRight + 1 < inputLines[coord.i].Length && char.IsDigit(inputLines[coord.i][toRight + 1]))
                        {
                            toRight++;
                            var rightCoord = new Coord { i = coord.i, j = toRight };
                            visited.Add(rightCoord);
                            numberDigits.Add(inputLines[rightCoord.i][rightCoord.j] - '0');
                        }

                        int number = 0;
                        foreach (int digit in numberDigits)
                        {
                            number = number * 10;
                            number += digit;
                        }

                        asterisk.NeighboringNumbers.Add(number);
                    }
                }
            }
        }
    }
}
