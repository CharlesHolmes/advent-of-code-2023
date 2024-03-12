namespace Day01Problem2.NumberWords
{
    public class NumberWordSearch
    {
        public NumberWordOccurrence? FirstOccurrence { get; private init; }
        public NumberWordOccurrence? LastOccurrence { get; private init; }

        public NumberWordSearch(string inputString)
        {
            var wordsInInput = NumberWord.AllNumberWords
                .Where(numberWord => inputString.Contains(numberWord.Word))
                .ToList();
            FirstOccurrence = wordsInInput
                .Select(numberWord => new NumberWordOccurrence(numberWord, inputString.IndexOf(numberWord.Word)))
                .OrderBy(occurrence => occurrence.Index)
                .FirstOrDefault();
            LastOccurrence = wordsInInput
                .Select(numberWord => new NumberWordOccurrence(numberWord, inputString.LastIndexOf(numberWord.Word)))
                .OrderBy(occurrence => occurrence.Index)
                .LastOrDefault();
        }
    }
}
