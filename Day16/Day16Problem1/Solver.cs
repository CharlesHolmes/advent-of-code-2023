namespace Day16Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var beams = new Queue<Beam>([ new Beam
            {
                Position = new Tile { RowIndex = 0, ColumnIndex = 0 },
                Direction = Direction.Right
            }]);

            var tileVisits = new Dictionary<Tile, HashSet<Direction>>
            {
                { beams.Peek().Position, new HashSet<Direction> { beams.Peek().Direction } }
            };

            while (beams.Any())
            {
                var current = beams.Dequeue();
                char tileContents = inputLines[current.Position.RowIndex][current.Position.ColumnIndex];
                List<Beam> allNextBeams = GetNextBeamPositions(current, tileContents);
                List<Beam> possibleNextBeams = FilterPositionsOutsideGrid(inputLines, allNextBeams);
                foreach (Beam beam in possibleNextBeams)
                {
                    if (!tileVisits.ContainsKey(beam.Position))
                    {
                        // never visited this tile
                        tileVisits.Add(beam.Position, new HashSet<Direction> { beam.Direction });
                        beams.Enqueue(beam);
                    }
                    else if (!tileVisits[beam.Position].Contains(beam.Direction))
                    {
                        // never visited this tile while traveling this direction
                        tileVisits[beam.Position].Add(beam.Direction);
                        beams.Enqueue(beam);
                    }
                    else
                    {
                        // already been on this tile while moving in this direction; ignore
                    }
                }
            }

            return tileVisits.Keys.Count;
        }

        private static List<Beam> FilterPositionsOutsideGrid(string[] inputLines, List<Beam> nextBeams)
        {
            return nextBeams
                .Where(beam => beam.Position.RowIndex >= 0
                    && beam.Position.RowIndex < inputLines.Length
                    && beam.Position.ColumnIndex >= 0
                    && beam.Position.ColumnIndex < inputLines[0].Length)
                .ToList();
        }

        private static List<Beam> GetNextBeamPositions(Beam current, char tileContents)
        {
            var nextBeamPositions = new List<Beam>();
            switch (tileContents)
            {
                case '.':
                    switch (current.Direction)
                    {
                        case Direction.Right:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex + 1 },
                                Direction = Direction.Right
                            });
                            break;
                        case Direction.Down:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex + 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Down
                            });
                            break;
                        case Direction.Left:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex - 1 },
                                Direction = Direction.Left
                            });
                            break;
                        case Direction.Up:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex - 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Up
                            });
                            break;
                    }
                    break;
                case '-':
                    switch (current.Direction)
                    {
                        case Direction.Right:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex + 1 },
                                Direction = Direction.Right
                            });
                            break;
                        case Direction.Left:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex - 1 },
                                Direction = Direction.Left
                            });
                            break;
                        case Direction.Down:
                        case Direction.Up:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex + 1 },
                                Direction = Direction.Right
                            });
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex - 1 },
                                Direction = Direction.Left
                            });
                            break;
                    }
                    break;
                case '|':
                    switch (current.Direction)
                    {
                        case Direction.Down:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex + 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Down
                            });
                            break;
                        case Direction.Up:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex - 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Up
                            });
                            break;
                        case Direction.Right:
                        case Direction.Left:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex - 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Up
                            });
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex + 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Down
                            });
                            break;
                    }
                    break;
                case '/':
                    switch (current.Direction)
                    {
                        case Direction.Right:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex - 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Up
                            });
                            break;
                        case Direction.Down:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex - 1 },
                                Direction = Direction.Left
                            });
                            break;
                        case Direction.Left:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex + 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Down
                            });
                            break;
                        case Direction.Up:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex + 1 },
                                Direction = Direction.Right
                            });
                            break;
                    }
                    break;
                case '\\':
                    switch (current.Direction)
                    {
                        case Direction.Left:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex - 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Up
                            });
                            break;
                        case Direction.Up:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex - 1 },
                                Direction = Direction.Left
                            });
                            break;
                        case Direction.Right:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex + 1, ColumnIndex = current.Position.ColumnIndex },
                                Direction = Direction.Down
                            });
                            break;
                        case Direction.Down:
                            nextBeamPositions.Add(new Beam
                            {
                                Position = new Tile { RowIndex = current.Position.RowIndex, ColumnIndex = current.Position.ColumnIndex + 1 },
                                Direction = Direction.Right
                            });
                            break;
                    }
                    break;
                default:
                    throw new ArgumentException("Unrecognized tile character");
            }

            return nextBeamPositions;
        }
    }
}
