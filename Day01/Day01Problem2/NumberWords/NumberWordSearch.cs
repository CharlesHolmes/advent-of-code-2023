namespace Day01Problem2.NumberWords
{
    public class NumberWordSearch
    {
        private readonly string _inputString;

        public NumberWordSearch(string inputString)
        {
            _inputString = inputString;
        }

        public NumberWordOccurrence? GetFirstNumberWordOccurrence()
        {
            NumberWordOccurrence? firstOccurrence = null;
            var wordsInInput = NumberWord.AllNumberWords.Where(nw => _inputString.Contains(nw.Word));
            foreach (NumberWord numberWord in wordsInInput)
            {
                int wordIndex = _inputString.IndexOf(numberWord.Word);
                if (firstOccurrence == null)
                {
                    firstOccurrence = new NumberWordOccurrence
                    {
                        NumberWord = numberWord,
                        Index = wordIndex
                    };
                }
                else
                {
                    if (wordIndex < firstOccurrence.Index)
                    {
                        firstOccurrence = new NumberWordOccurrence
                        {
                            NumberWord = numberWord,
                            Index = wordIndex
                        };
                    }
                }
            }

            return firstOccurrence;
        }

        public NumberWordOccurrence? GetLastNumberWordOccurrence()
        {
            NumberWordOccurrence? lastOccurrence = null;
            var wordsInInput = NumberWord.AllNumberWords.Where(nw => _inputString.Contains(nw.Word));
            foreach (NumberWord numberWord in wordsInInput)
            {
                int wordIndex = _inputString.LastIndexOf(numberWord.Word);
                if (lastOccurrence == null)
                {
                    lastOccurrence = new NumberWordOccurrence
                    {
                        NumberWord = numberWord,
                        Index = wordIndex
                    };
                }
                else
                {
                    if (wordIndex > lastOccurrence.Index)
                    {
                        lastOccurrence = new NumberWordOccurrence
                        {
                            NumberWord = numberWord,
                            Index = wordIndex
                        };
                    }
                }
            }

            return lastOccurrence;
        }
    }
}
