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
                        var location = new Coord
                        {
                            RowIndex = i,
                            ColumnIndex = j
                        };
                        symbolList.Add(new Symbol
                        {
                            Location = location,
                            Character = _gridChars[i][j],
                            NeighboringNumbers = GetNeighboringNumbers(location)
                        });
                    }
                }
            }

            Symbols = symbolList.ToImmutableList();
        }

        private ImmutableList<int> GetNeighboringNumbers(Coord symbolLocation)
        {
            var result = new List<int>();
            var visited = new HashSet<Coord>();
            var neighbors = GetNeighborCoordinates(symbolLocation);
            foreach (Coord coord in neighbors)
            {
                if (!visited.Contains(coord))
                {
                    if (char.IsDigit(_gridChars[coord.RowIndex][coord.ColumnIndex]))
                    {
                        result.Add(int.Parse(GetFullNumberString(visited, coord.RowIndex, coord.ColumnIndex)));
                    }
                    visited.Add(coord);
                }
            }            

            return result.ToImmutableList();
        }

        private string GetFullNumberString(HashSet<Coord> visited, int rowIndex, int columnIndex)
        {
            var current = new Coord { RowIndex = rowIndex, ColumnIndex = columnIndex };
            if (!visited.Contains(current) && IsValidGridCoordinate(current))
            {
                visited.Add(current);
                if (char.IsDigit(_gridChars[rowIndex][columnIndex]))
                {
                    return GetFullNumberString(visited, rowIndex, columnIndex - 1)
                        + _gridChars[rowIndex][columnIndex].ToString()
                        + GetFullNumberString(visited, rowIndex, columnIndex + 1);
                }
            }

            return string.Empty;
        }

        private IEnumerable<Coord> GetNeighborCoordinates(Coord symbolLocation) 
        {
            return GetUnvalidatedPotentialNeighbors(symbolLocation)
                .Where(neighborLocation =>
                    !neighborLocation.Equals(symbolLocation)
                    && IsValidGridCoordinate(neighborLocation));
        }

        private IEnumerable<Coord> GetUnvalidatedPotentialNeighbors(Coord symbolLocation)
        {
            return Enumerable.Range(-1, 3).SelectMany(rowOffset =>
                Enumerable.Range(-1, 3).Select(columnOffset =>
                    new Coord
                    {
                        RowIndex = symbolLocation.RowIndex + rowOffset,
                        ColumnIndex = symbolLocation.ColumnIndex + columnOffset
                    }));
        }

        private bool IsValidGridCoordinate(Coord coord) =>
            coord.RowIndex >= 0
            && coord.ColumnIndex >= 0
            && coord.RowIndex < _gridChars.Length
            && coord.ColumnIndex < _gridChars[coord.RowIndex].Length;
    }
}
