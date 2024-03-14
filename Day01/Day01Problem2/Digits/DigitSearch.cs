namespace Day01Problem2.Digits
{
    public class DigitSearch : IDigitSearch
    {
        public DigitOccurrence? FirstDigitOccurrence { get; private init; }
        public DigitOccurrence? LastDigitOccurence { get; private init; }

        public DigitSearch(string inputString)
        {
            var digitOccurrences = inputString
                .Select((c, i) => new DigitOccurrence(c, i))
                .Where(x => char.IsDigit(x.DigitChar))
                .ToArray();
            FirstDigitOccurrence = digitOccurrences.FirstOrDefault();
            LastDigitOccurence = digitOccurrences.LastOrDefault();
        }
    }
}
