using System.Collections.Immutable;

namespace Day01Problem2.NumberWords
{
    public class NumberWord
    {
        public string Word { get; init; }
        public int Value { get; init; }

        public NumberWord(string word, int val)
        {
            Word = word;
            Value = val;
        }

        public static readonly ImmutableList<NumberWord> AllNumberWords = ImmutableList.Create(
            new NumberWord("one", 1),
            new NumberWord("two", 2),
            new NumberWord("three", 3),
            new NumberWord("four", 4),
            new NumberWord("five", 5),
            new NumberWord("six", 6),
            new NumberWord("seven", 7),
            new NumberWord("eight", 8),
            new NumberWord("nine", 9));
    }
}
