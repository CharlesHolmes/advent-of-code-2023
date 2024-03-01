namespace Day17Problem1
{
    public readonly struct PathStepKey
    {
        public Block Position { get; init; }
        public Direction LastMove { get; init; }
        public Direction SecondLastMove { get; init; }
        public Direction ThirdLastMove { get; init; }
    }
}
