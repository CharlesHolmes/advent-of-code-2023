using System.Collections.Immutable;

namespace Day03Problem1
{
    public class Grid
    {
        private ImmutableArray<ImmutableArray<char>> _gridChars { get; init; } = ImmutableArray<ImmutableArray<char>>.Empty;
        public ImmutableList<PartNumber> PartNumbers { get; private set; } = ImmutableList.Create<PartNumber>();

        private Grid()
        {
        }

        public static Grid ParseInput(string[] inputLines)
        {
            var grid = new Grid
            {
                _gridChars = inputLines.Select(line => line.ToImmutableArray()).ToImmutableArray()
            };

            grid.IdentifyNumbers();
            return grid;
        }

        private void IdentifyNumbers()
        {
            var partNumbers = new List<PartNumber>();
            for (int i = 0; i < _gridChars.Length; i++)
            {
                var partNumberToAdd = new PartNumber();
                for (int j = 0; j < _gridChars[i].Length; j++)
                {
                    if (char.IsDigit(_gridChars[i][j]))
                    {
                        partNumberToAdd.Number *= 10;
                        partNumberToAdd.Number += _gridChars[i][j] - '0';
                        if (DoesPositionHaveAdjacentSymbol(i, j))
                        {
                            partNumberToAdd.HasAdjacentSymbol = true;
                        }
                    }
                    else
                    {
                        if (partNumberToAdd.Number > 0)
                        {
                            partNumbers.Add(partNumberToAdd);
                            partNumberToAdd = new PartNumber();
                        }
                    }
                }

                if (partNumberToAdd.Number > 0)
                {
                    partNumbers.Add(partNumberToAdd);
                }
            }

            PartNumbers = partNumbers.ToImmutableList();
        }

        private bool DoesPositionHaveAdjacentSymbol(int i, int j)
        {
            for (int idelta = -1; idelta <= 1; idelta++)
            {
                for (int jdelta = -1; jdelta <= 1; jdelta++)
                {
                    if (i + idelta < 0) continue;
                    if (i + idelta >= _gridChars.Length) continue;
                    if (j + jdelta < 0) continue;
                    if (j + jdelta >= _gridChars[i + idelta].Length) continue;
                    char c = _gridChars[i + idelta][j + jdelta];
                    if (!char.IsDigit(c) && c != '.')
                        return true;
                }
            }

            return false;
        }
    }
}
