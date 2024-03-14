namespace Day01Problem2.Digits
{
    public interface IDigitSearchFactory
    {
        IDigitSearch Create(string inputString);
    }
}