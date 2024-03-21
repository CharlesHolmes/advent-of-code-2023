using Day01Problem2.Digits;
using Day01Problem2.NumberWords;

namespace Day01Problem2.InputLines
{
    public class InputLine : IInputLine
    {
        private readonly INumberWordSearch _numberWordSearch;
        private readonly IDigitSearch _digitSearch;

        public InputLine(
            string inputString,
            IDigitSearchFactory digitSearchFactory,
            INumberWordSearchFactory numberWordSearchFactory)
        {
            _numberWordSearch = numberWordSearchFactory.Create(inputString);
            _digitSearch = digitSearchFactory.Create(inputString);
        }

        public int GetLineValue()
        {
            return GetFirstWordOrDigitValue() * 10 + GetLastWordOrDigitValue();
        }

        private int GetFirstWordOrDigitValue()
        {
            NumberWordOccurrence? firstWordOccurrence = _numberWordSearch.FirstOccurrence;
            DigitOccurrence? firstDigitOccurrence = _digitSearch.FirstDigitOccurrence;
            if (firstDigitOccurrence != null && firstWordOccurrence != null)
            {
                if (firstDigitOccurrence.Index < firstWordOccurrence.Index)
                {
                    return firstDigitOccurrence.DigitValue;
                }
                else
                {
                    return firstWordOccurrence.NumberWord.Value;
                }
            }
            else
            {
                return GetNonNullOccurrenceValue(firstWordOccurrence, firstDigitOccurrence);
            }
        }

        private int GetLastWordOrDigitValue()
        {
            NumberWordOccurrence? lastWordOccurrence = _numberWordSearch.LastOccurrence;
            DigitOccurrence? lastDigitOccurrence = _digitSearch.LastDigitOccurrence;
            if (lastDigitOccurrence != null && lastWordOccurrence != null)
            {
                if (lastDigitOccurrence.Index > lastWordOccurrence.Index)
                {
                    return lastDigitOccurrence.DigitValue;
                }
                else
                {
                    return lastWordOccurrence.NumberWord.Value;
                }
            }
            else
            {
                return GetNonNullOccurrenceValue(lastWordOccurrence, lastDigitOccurrence);
            }
        }

        private static int GetNonNullOccurrenceValue(NumberWordOccurrence? wordOccurrence, DigitOccurrence? digitOccurrence)
        {
            if (wordOccurrence != null)
            {
                return wordOccurrence.NumberWord.Value;
            }
            else if (digitOccurrence != null)
            {
                return digitOccurrence.DigitValue;
            }
            else
            {
                throw new InvalidOperationException("Line contained neither a digit nor a word!");
            }
        }
    }
}
