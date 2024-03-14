namespace Day01Problem2.NumberWords
{
    public class NumberWordSearchFactory : INumberWordSearchFactory
    {
        public INumberWordSearch Create(string inputString) => new NumberWordSearch(inputString);
    }
}
