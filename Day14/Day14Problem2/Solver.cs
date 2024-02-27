namespace Day14Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            char[][] mutable = inputLines.Select(s => s.ToCharArray()).ToArray();
            const int maxCycles = 1000000000;
            var recentBoards = new List<string>();
            var seen = new HashSet<string>();
            for (int i = 1; i <= maxCycles; i++)
            {
                Cycle(mutable);
                string asString = string.Join("", mutable.Select(arr => string.Join("", arr)));
                if (seen.Contains(asString))
                {
                    // found a repeating pattern
                    // find out what it would look like on the billionth step and break out
                    int patternLength = 1;
                    while (!string.Equals(recentBoards[recentBoards.Count - patternLength], asString, StringComparison.Ordinal))
                    {
                        patternLength++;
                    }

                    int remainingSteps = maxCycles - i;
                    int offsetInRecent = remainingSteps % patternLength;
                    string billionthBoardString = recentBoards[recentBoards.Count - patternLength + offsetInRecent];
                    mutable = RecoverArrFromString(billionthBoardString, mutable.Length, mutable[0].Length);
                    break;
                }
                else
                {
                    seen.Add(asString);
                    recentBoards.Add(asString);
                }
            }

            long weightCount = CountWeight(mutable);
            return weightCount;
        }

        private static long CountWeight(char[][] input)
        {
            long weight = 0;
            for (int i = 0; i < input.Length; i++)
            {
                long rowItemWeight = input.Length - i;
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i][j] == 'O')
                    {
                        weight += rowItemWeight;
                    }
                }
            }

            return weight;
        }

        private static void MoveRockNorth(char[][] input, int column, int row)
        {
            while (row - 1 >= 0 && input[row - 1][column] == '.')
            {
                input[row][column] = '.';
                input[row - 1][column] = 'O';
                row--;
            }
        }

        private static void MoveRockSouth(char[][] input, int column, int row)
        {
            while (row + 1 < input.Length && input[row + 1][column] == '.')
            {
                input[row][column] = '.';
                input[row + 1][column] = 'O';
                row++;
            }
        }

        private static void MoveRockWest(char[][] input, int column, int row)
        {
            while (column - 1 >= 0 && input[row][column - 1] == '.')
            {
                input[row][column] = '.';
                input[row][column - 1] = 'O';
                column--;
            }
        }

        private static void MoveRockEast(char[][] input, int column, int row)
        {
            while (column + 1 < input[row].Length && input[row][column + 1] == '.')
            {
                input[row][column] = '.';
                input[row][column + 1] = 'O';
                column++;
            }
        }

        private static int FindRockInColumnSearchingDown(char[][] input, int column, int startAt)
        {
            for (int i = startAt; i < input.Length; i++)
            {
                if (input[i][column] == 'O')
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindRockInColumnSearchingUp(char[][] input, int column, int startAt)
        {
            for (int i = startAt; i >= 0; i--)
            {
                if (input[i][column] == 'O')
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindRockInRowSearchingRight(char[][] input, int row, int startAt)
        {
            for (int j = startAt; j < input[row].Length; j++)
            {
                if (input[row][j] == 'O')
                {
                    return j;
                }
            }

            return -1;
        }

        private static int FindRockInRowSearchingLeft(char[][] input, int row, int startAt)
        {
            for (int j = startAt; j >= 0; j--)
            {
                if (input[row][j] == 'O')
                {
                    return j;
                }
            }

            return -1;
        }

        private static void MoveNorth(char[][] input)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                int startAt = 0;
                int rockToMoveRow;
                while ((rockToMoveRow = FindRockInColumnSearchingDown(input, j, startAt)) >= 0)
                {
                    MoveRockNorth(input, j, rockToMoveRow);
                    startAt = rockToMoveRow + 1;
                }
            }
        }

        private static void MoveWest(char[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int startAt = 0;
                int rockToMoveColumn;
                while ((rockToMoveColumn = FindRockInRowSearchingRight(input, i, startAt)) >= 0)
                {
                    MoveRockWest(input, rockToMoveColumn, i);
                    startAt = rockToMoveColumn + 1;
                }
            }
        }

        private static void MoveSouth(char[][] input)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                int startAt = input.Length - 1;
                int rockToMoveRow;
                while ((rockToMoveRow = FindRockInColumnSearchingUp(input, j, startAt)) >= 0)
                {
                    MoveRockSouth(input, j, rockToMoveRow);
                    startAt = rockToMoveRow - 1;
                }
            }
        }

        private static void MoveEast(char[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int startAt = input[0].Length - 1;
                int rockToMoveColumn;
                while ((rockToMoveColumn = FindRockInRowSearchingLeft(input, i, startAt)) >= 0)
                {
                    MoveRockEast(input, rockToMoveColumn, i);
                    startAt = rockToMoveColumn - 1;
                }
            }
        }

        private static void Cycle(char[][] input)
        {
            MoveNorth(input);
            MoveWest(input);
            MoveSouth(input);
            MoveEast(input);
        }

        private static char[][] RecoverArrFromString(string input, int rows, int cols)
        {
            int charIndex = 0;
            char[][] result = new char[rows][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new char[cols];
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = input[charIndex++];
                }
            }

            return result;
        }
    }
}
