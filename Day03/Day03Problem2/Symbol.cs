using System.Collections.Immutable;

namespace Day03Problem2
{
    public class Symbol
    {
        public Coord Location { get; init; }
        public char Character { get; init; }
        public ImmutableList<int> NeighboringNumbers { get; init; } = ImmutableList.Create<int>();
        public bool IsGear() => Character == '*' && NeighboringNumbers.Count == 2;
        public long GetGearRatio() => NeighboringNumbers.Aggregate(1L, (x, y) => x * y);
    }
}
