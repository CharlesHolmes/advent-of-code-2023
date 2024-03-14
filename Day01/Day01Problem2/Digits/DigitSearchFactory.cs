namespace Day01Problem2.Digits
{
    public class DigitSearchFactory : IDigitSearchFactory
    {
        public IDigitSearch Create(string inputString) => new DigitSearch(inputString);
    }
}
