namespace Day10Problem2
{
    public struct Coord
    {
        public int Row;
        public int Col;
    }

    internal class Program
    {
        private static Coord GetNextStep(char c, Coord current, Coord last)
        {
            var up = new Coord { Col = current.Col, Row = current.Row - 1 };
            var down = new Coord { Col = current.Col, Row = current.Row + 1 };
            var left = new Coord { Col = current.Col - 1, Row = current.Row };
            var right = new Coord { Col = current.Col + 1, Row = current.Row };
            switch (c)
            {
                case '|':
                    if (up.Equals(last)) return down;
                    else if (down.Equals(last)) return up;
                    else throw new Exception("Should not have gotten here.");
                case '-':
                    if (left.Equals(last)) return right;
                    else if (right.Equals(last)) return left;
                    else throw new Exception("Should not have gotten here.");
                case 'L':
                    if (up.Equals(last)) return right;
                    else if (right.Equals(last)) return up;
                    else throw new Exception("Should not have gotten here.");
                case 'J':
                    if (up.Equals(last)) return left;
                    else if (left.Equals(last)) return up;
                    else throw new Exception("Should not have gotten here.");
                case '7':
                    if (left.Equals(last)) return down;
                    else if (down.Equals(last)) return left;
                    else throw new Exception("Should not have gotten here.");
                case 'F':
                    if (down.Equals(last)) return right;
                    else if (right.Equals(last)) return down;
                    else throw new Exception("Should not have gotten here.");
            }

            throw new Exception("unrecognized char");
        }

        private static bool[][] IdentifyLoop(Coord startingTile, string[] inputLines)
        {
            Coord last = new Coord { Row = -1, Col = -1 };
            Coord current = new Coord { Row = -1, Col = -1 };
            if (startingTile.Row > 0)
            {
                char above = inputLines[startingTile.Row - 1][startingTile.Col];
                char right = inputLines[startingTile.Row][startingTile.Col + 1];
                char below = inputLines[startingTile.Row + 1][startingTile.Col];
                char left = inputLines[startingTile.Row][startingTile.Col - 1];
                if (above == '|' || above == '7' || above == 'F')
                {
                    current = new Coord { Row = startingTile.Row - 1, Col = startingTile.Col };
                    last = startingTile;
                }
                else if (right == '-' || right == 'J' || right == '7')
                {
                    current = new Coord { Row = startingTile.Row, Col = startingTile.Col + 1 };
                    last = startingTile;
                }
                else if (below == '|' || below == 'L' || below == 'J')
                {
                    current = new Coord { Row = startingTile.Row + 1, Col = startingTile.Col };
                    last = startingTile;

                }
                else if (left == '-' || left == 'L' || left == 'F')
                {
                    current = new Coord { Row = startingTile.Row, Col = startingTile.Col - 1 };
                    last = startingTile;
                }
                else
                {
                    throw new Exception("nope");
                }
            }

            bool[][] marked = new bool[inputLines.Length][];
            for (int i = 0; i < marked.Length; i++)
            {
                marked[i] = new bool[inputLines[0].Length];
            }

            marked[startingTile.Row][startingTile.Col] = true;
            while (!current.Equals(startingTile))
            {
                marked[current.Row][current.Col] = true;
                Coord temp = current;
                current = GetNextStep(inputLines[current.Row][current.Col], current, last);
                last = temp;
            }

            return marked;
        }

        private static int CountTilesInsideLoop(Coord startingTile, string[] inputLines)
        {
            int insideCount = 0;
            bool[][] marked = IdentifyLoop(startingTile, inputLines);
            for (int i = 0; i < inputLines.Length; i++)
            {
                int connectedToAboveCount = 0;
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    char c = inputLines[i][j];
                    if (marked[i][j])
                    {
                        if (c == '|' || c == 'L' || c == 'J')
                        {
                            connectedToAboveCount++;
                        }
                    }
                    else
                    {
                        if (connectedToAboveCount % 2 == 1)
                        {
                            insideCount++;
                        }
                    }
                }
            }

            return insideCount;
        }

        static async Task Main(string[] args)
        {
            Coord startingTile = new Coord { Row = -1, Col = -1 };
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            for (int row = 0; row < inputLines.Length; row++)
            {
                for (int col = 0; col < inputLines[row].Length; col++)
                {
                    if (inputLines[row][col] == 'S') startingTile = new Coord
                    {
                        Row = row,
                        Col = col
                    };
                }
            }

            int insideCount = CountTilesInsideLoop(startingTile, inputLines);
            Console.Out.WriteLine(insideCount);
        }
    }
}
