namespace Day17Problem2
{
    public readonly struct PathStepKey
    {
        public Block Position { get; init; }
        public List<Direction> LastMoves { get; init; }
        public string LastMovesKey
        {
            get
            {
                return string.Join(",", LastMoves);
            }
        }
    }
}
