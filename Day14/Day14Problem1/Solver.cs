namespace Day14Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            char[][] mutable = inputLines.Select(s => s.ToCharArray()).ToArray();
            MoveNorth(mutable);
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

        private static int FindRockInColumn(char[][] input, int column, int startAt)
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

        private static void MoveNorth(char[][] input)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                int startAt = 0;
                int rockToMoveRow;
                while ((rockToMoveRow = FindRockInColumn(input, j, startAt)) >= 0)
                {
                    MoveRockNorth(input, j, rockToMoveRow);
                    startAt = rockToMoveRow + 1;
                }
            }
        }
    }
}
