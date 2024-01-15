namespace Day03Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var symbolList = FindSymbols(inputLines);
            foreach (Symbol symbol in symbolList)
            {
                PopulateNeighboringNumbers(symbol, inputLines);
            }

            return symbolList
                .Where(x => x.IsGear())
                .Sum(x => x.GetGearRatio());
        }

        // TODO - introduce the notion of a grid and abstract finding and populating

        private static List<Symbol> FindSymbols(string[] inputLines)
        {
            var symbolList = new List<Symbol>();
            for (int i = 0; i < inputLines.Length; i++)
            {
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    if (inputLines[i][j] != '.' && !char.IsDigit(inputLines[i][j]))
                    {
                        symbolList.Add(new Symbol
                        {
                            i = i,
                            j = j,
                            Character = inputLines[i][j]
                        });
                    }
                }
            }

            return symbolList;
        }

        private static void PopulateNeighboringNumbers(Symbol symbol, string[] inputLines)
        {
            // find surrounding numbers
            var visited = new HashSet<Coord>();
            for (int idelta = -1; idelta <= 1; idelta++)
            {
                for (int jdelta = -1; jdelta <= 1; jdelta++)
                {
                    if (symbol.i + idelta < 0) continue;
                    else if (symbol.j + jdelta < 0) continue;
                    else if (symbol.i + idelta >= inputLines.Length) continue;
                    else if (symbol.j + jdelta >= inputLines[symbol.i + idelta].Length) continue;

                    var coord = new Coord { i = symbol.i + idelta, j = symbol.j + jdelta };
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

                        symbol.NeighboringNumbers.Add(number);
                    }
                }
            }
        }
    }
}
