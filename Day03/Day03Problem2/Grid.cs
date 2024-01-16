using System.Collections.Immutable;

namespace Day03Problem2
{
    public class Grid
    {
        private ImmutableArray<ImmutableArray<char>> _gridChars { get; init; } = ImmutableArray<ImmutableArray<char>>.Empty;
        public ImmutableList<Symbol> Symbols { get; private set; } = ImmutableList.Create<Symbol>();

        private Grid()
        {
        }

        public static Grid Parse(string[] inputLines)
        {
            var grid = new Grid
            {
                _gridChars = inputLines.Select(line => line.ToImmutableArray()).ToImmutableArray()
            };

            grid.IdentifySymbols();
            return grid;
        }

        private void IdentifySymbols()
        {
            var symbolList = new List<Symbol>();
            for (int i = 0; i < _gridChars.Length; i++)
            {
                for (int j = 0; j < _gridChars[i].Length; j++)
                {
                    if (_gridChars[i][j] != '.' && !char.IsDigit(_gridChars[i][j]))
                    {
                        symbolList.Add(new Symbol
                        {
                            RowIndex = i,
                            ColumnIndex = j,
                            Character = _gridChars[i][j],
                            NeighboringNumbers = GetNeighboringNumbers(i, j)
                        });
                    }
                }
            }

            Symbols = symbolList.ToImmutableList();
        }

        private ImmutableList<int> GetNeighboringNumbers(int symbolRow, int symbolColumn)
        {
            var result = new List<int>();
            var visited = new HashSet<Coord>();
            var neighbors = GetNeighborCoordinates(symbolRow, symbolColumn);
            foreach (Coord coord in neighbors)
            {
                if (!visited.Contains(coord))
                {
                    visited.Add(coord);
                    if (char.IsDigit(_gridChars[coord.rowIndex][coord.columnIndex]))
                    {
                        result.Add(GetFullNumber(visited, coord));
                    }
                }
            }            

            return result.ToImmutableList();
        }

        private int GetFullNumber(HashSet<Coord> visited, Coord coord)
        {
            var numberDigits = new List<int>
            {
                ConvertToNumeric(_gridChars[coord.rowIndex][coord.columnIndex])
            };
            int toLeft = coord.columnIndex;
            while (toLeft - 1 >= 0 && char.IsDigit(_gridChars[coord.rowIndex][toLeft - 1]))
            {
                toLeft--;
                var leftCoord = new Coord { rowIndex = coord.rowIndex, columnIndex = toLeft };
                visited.Add(leftCoord);
                numberDigits.Insert(0, ConvertToNumeric(_gridChars[leftCoord.rowIndex][leftCoord.columnIndex]));
            }

            int toRight = coord.columnIndex;
            while (toRight + 1 < _gridChars[coord.rowIndex].Length && char.IsDigit(_gridChars[coord.rowIndex][toRight + 1]))
            {
                toRight++;
                var rightCoord = new Coord { rowIndex = coord.rowIndex, columnIndex = toRight };
                visited.Add(rightCoord);
                numberDigits.Add(ConvertToNumeric(_gridChars[rightCoord.rowIndex][rightCoord.columnIndex]));
            }

            int number = 0;
            foreach (int digit in numberDigits)
            {
                number *= 10;
                number += digit;
            }

            return number;
        }

        private List<Coord> GetNeighborCoordinates(int rowIndex, int columnIndex) 
        {
            var result = new List<Coord>();
            for (int idelta = -1; idelta <= 1; idelta++)
            {
                for (int jdelta = -1; jdelta <= 1; jdelta++)
                {
                    if (idelta == 0 && jdelta == 0) continue;
                    else if (rowIndex + idelta < 0) continue;
                    else if (columnIndex + jdelta < 0) continue;
                    else if (rowIndex + idelta >= _gridChars.Length) continue;
                    else if (columnIndex + jdelta >= _gridChars[rowIndex + idelta].Length) continue;
                    result.Add(new Coord { rowIndex = rowIndex + idelta, columnIndex = columnIndex + jdelta });
                }
            }

            return result;
        }

        private static int ConvertToNumeric(char c)
        {
            return c - '0';
        }
    }
}
