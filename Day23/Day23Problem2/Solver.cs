namespace Day23Problem2
{
    public class Solver
    {
        public int GetSolution(string[] maze)
        {
            bool[][] visited = new bool[maze.Length][];
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = new bool[maze[i].Length];
            }

            int? startCol = null;
            for (int j = 0; j < maze[0].Length; j++)
            {
                if (maze[0][j] == '.') startCol = j;
            }

            if (!startCol.HasValue) throw new ArgumentException("Couldn't find start position");
            int bestLength = 0;
            FindPathLengths(maze, 0, startCol.Value, visited, 0, ref bestLength);
            return bestLength;
        }

        private static void FindPathLengths(string[] maze, int i, int j, bool[][] visited, int currentPathLength, ref int bestLength)
        {
            if (i < 0 || i >= maze.Length || j < 0 || j >= maze[0].Length || maze[i][j] == '#' || visited[i][j])
                return;

            if (i == maze.Length - 1)
            {
                if (currentPathLength > bestLength)
                {
                    bestLength = currentPathLength;
                }
            }
            else
            {
                visited[i][j] = true;
                if (maze[i][j] == '.' || maze[i][j] == '>' || maze[i][j] == 'v')
                {
                    FindPathLengths(maze, i - 1, j, visited, currentPathLength + 1, ref bestLength);
                    FindPathLengths(maze, i + 1, j, visited, currentPathLength + 1, ref bestLength);
                    FindPathLengths(maze, i, j - 1, visited, currentPathLength + 1, ref bestLength);
                    FindPathLengths(maze, i, j + 1, visited, currentPathLength + 1, ref bestLength);
                }
                else throw new ArgumentException("Unrecognized maze tile."); // the left and up cases don't seem to exist in my input
                visited[i][j] = false;
            }
        }
    }
}
