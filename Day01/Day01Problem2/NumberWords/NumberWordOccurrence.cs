namespace Day01Problem2.NumberWords
{
    public class NumberWordOccurrence
    {
        public NumberWord NumberWord { get; private init; }
        public int Index { get; private init; }

        public NumberWordOccurrence(NumberWord numberWord, int index)
        {
            NumberWord = numberWord;
            Index = index;
        }
    }
}
