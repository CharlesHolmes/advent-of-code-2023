using System.Collections.Immutable;

namespace Day01Problem1
{
    public class Line : ILine
    {
        private readonly long _lineValue;

        public Line(string input)
        {
            ImmutableArray<char> digits = input.Where(c => char.IsDigit(c)).ToImmutableArray();
            int firstDigit = digits.First() - '0';
            int lastDigit = digits.Last() - '0';
            _lineValue = firstDigit * 10 + lastDigit;
        }

        public long GetLineValue() => _lineValue;
    }
}
