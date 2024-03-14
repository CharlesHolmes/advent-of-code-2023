namespace Day01Problem2.NumberWords
{
    public interface INumberWordSearchFactory
    {
        INumberWordSearch Create(string inputString);
    }
}