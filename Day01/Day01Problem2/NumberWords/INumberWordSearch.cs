namespace Day01Problem2.NumberWords
{
    public interface INumberWordSearch
    {
        NumberWordOccurrence? FirstOccurrence { get; }
        NumberWordOccurrence? LastOccurrence { get; }
    }
}