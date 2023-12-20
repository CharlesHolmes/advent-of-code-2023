namespace Day13Problem2
{

    internal class Program
    {
        private static List<string[]> ParseGrids(string[] inputLines)
        {
            var grids = new List<string[]>();
            var gridBuffer = new List<string>();
            foreach (string line in inputLines)
            {
                if (line.Length > 0)
                {
                    gridBuffer.Add(line);
                }
                else
                {
                    grids.Add(gridBuffer.ToArray());
                    gridBuffer.Clear();
                }
            }

            if (gridBuffer.Count > 0)
            {
                grids.Add(gridBuffer.ToArray());
                gridBuffer.Clear();
            }

            return grids;
        }

        private static int? GetCountOfColumnsLeftOfVerticalSplitOrDefault(string[] grid)
        {
            for (int j = 0; j + 1 < grid[0].Length; j++)
            {
                int smudges = 0;
                int left = j;
                int right = j + 1;
                while (left >= 0 && right < grid[0].Length)
                {
                    for (int i = 0; smudges <= 1 && i < grid.Length; i++)
                    {
                        if (grid[i][left] != grid[i][right]) smudges++;
                    }

                    left--;
                    right++;
                }

                if (smudges == 1) return j + 1;
            }

            return null;
        }

        private static int? GetCountOfRowsAboveHorizontalSplitOrDefault(string[] grid)
        {
            for (int i = 0; i + 1 < grid.Length; i++)
            {
                int smudges = 0;
                int up = i;
                int down = i + 1;
                while (up >= 0 && down < grid.Length)
                {
                    for (int j = 0; smudges <= 1 && j < grid[i].Length; j++)
                    {
                        if (grid[up][j] != grid[down][j]) smudges++;
                    }

                    up--;
                    down++;
                }

                if (smudges == 1) return i + 1;
            }

            return default;
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            List<string[]> grids = ParseGrids(inputLines);
            long totalScore = 0;
            foreach (string[] grid in grids)
            {
                int? columnsLeftOfSplit = GetCountOfColumnsLeftOfVerticalSplitOrDefault(grid);
                int? rowsAboveSplit = GetCountOfRowsAboveHorizontalSplitOrDefault(grid);
                if (columnsLeftOfSplit.HasValue && rowsAboveSplit.HasValue) throw new Exception("Did not expect this.");
                else if (columnsLeftOfSplit.HasValue)
                {
                    totalScore += columnsLeftOfSplit.Value;
                }
                else if (rowsAboveSplit.HasValue)
                {
                    totalScore += 100 * rowsAboveSplit.Value;
                }
                else
                {
                    throw new Exception("Was this supposed to happen?");
                }
            }

            Console.WriteLine(totalScore);
        }
    }
}
