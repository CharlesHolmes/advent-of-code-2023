namespace Day01Problem2.Digits
{
    public interface IDigitSearch
    {
        DigitOccurrence? FirstDigitOccurrence { get; }
        DigitOccurrence? LastDigitOccurrence { get; }
    }
}