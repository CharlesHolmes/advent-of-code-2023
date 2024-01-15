using System.Collections.Immutable;

namespace Day03Problem2
{
    public class Symbol
    {
        public int i { get; init; }
        public int j { get; init; }
        public char Character { get; init; }
        public List<int> NeighboringNumbers { get; init; } = new List<int>();
        public bool IsGear() => Character == '*' && NeighboringNumbers.Count == 2;
        public long GetGearRatio() => NeighboringNumbers.Aggregate(1L, (x, y) => x * y);
    }
}
