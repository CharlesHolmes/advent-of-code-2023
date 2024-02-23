﻿namespace Day10Problem1
{
    public class Solver
    {
        private static readonly string BAD_PIPES_LAYOUT_MESSAGE = "Reached an undefined point in the input pipes.";

        public long GetSolution(string[] inputLines)
        {
            Coord startingTile = new Coord { Row = -1, Col = -1 };
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
                    throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
                }
            }

            long steps = 1;
            while (!current.Equals(startingTile))
            {
                Coord temp = current;
                current = GetNextStep(inputLines[current.Row][current.Col], current, last);
                last = temp;
                steps++;
            }

            long result = steps / 2;
            return result;
        }

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
                    else throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
                case '-':
                    if (left.Equals(last)) return right;
                    else if (right.Equals(last)) return left;
                    else throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
                case 'L':
                    if (up.Equals(last)) return right;
                    else if (right.Equals(last)) return up;
                    else throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
                case 'J':
                    if (up.Equals(last)) return left;
                    else if (left.Equals(last)) return up;
                    else throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
                case '7':
                    if (left.Equals(last)) return down;
                    else if (down.Equals(last)) return left;
                    else throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
                case 'F':
                    if (down.Equals(last)) return right;
                    else if (right.Equals(last)) return down;
                    else throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
            }

            throw new InvalidOperationException(BAD_PIPES_LAYOUT_MESSAGE);
        }
    }
}
