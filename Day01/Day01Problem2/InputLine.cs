namespace Day01Problem2
{
    public class InputLine
    {
        private readonly List<NumberWord> _numberWords = new List<NumberWord>
        {
            new NumberWord { Word = "one", Value = 1 },
            new NumberWord { Word = "two", Value = 2 },
            new NumberWord { Word = "three", Value = 3 },
            new NumberWord { Word = "four", Value = 4 },
            new NumberWord { Word = "five", Value = 5 },
            new NumberWord { Word = "six", Value = 6 },
            new NumberWord { Word = "seven", Value = 7 },
            new NumberWord { Word = "eight", Value = 8 },
            new NumberWord { Word = "nine", Value = 9 },
        };

        private readonly string _inputString;

        public InputLine(string inputString)
        {
            _inputString = inputString;
        }

        public int GetLineValue()
        {
            return GetFirstWordOrDigitValue() * 10 + GetLastWordOrDigitValue();
        }

        private int GetFirstWordOrDigitValue()
        {
            NumberWordOccurrence? firstWordOccurrence = GetFirstNumberWordOccurrence();
            DigitOccurrence? firstDigitOccurrence = GetFirstDigitOccurrence();
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
            NumberWordOccurrence? lastWordOccurrence = GetLastNumberWordOccurrence();
            DigitOccurrence? lastDigitOccurrence = GetLastDigitOccurrence();
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

        private int GetNonNullOccurrenceValue(NumberWordOccurrence? wordOccurrence, DigitOccurrence? digitOccurrence)
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

        private NumberWordOccurrence? GetFirstNumberWordOccurrence()
        {
            NumberWordOccurrence? firstOccurrence = null;
            foreach (NumberWord numberWord in _numberWords)
            {
                if (_inputString.Contains(numberWord.Word))
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
            }

            return firstOccurrence;
        }

        private DigitOccurrence? GetFirstDigitOccurrence()
        {
            for (int i = 0; i < _inputString.Length; i++)
            {
                if (char.IsDigit(_inputString[i])) return new DigitOccurrence
                {
                    DigitChar = _inputString[i],
                    Index = i
                };
            }

            return null;
        }

        private NumberWordOccurrence? GetLastNumberWordOccurrence()
        {
            NumberWordOccurrence? lastOccurrence = null;
            foreach (NumberWord numberWord in _numberWords)
            {
                if (_inputString.Contains(numberWord.Word))
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
            }

            return lastOccurrence;
        }

        private DigitOccurrence? GetLastDigitOccurrence()
        {
            for (int i = _inputString.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(_inputString[i])) return new DigitOccurrence
                {
                    DigitChar = _inputString[i],
                    Index = i
                };
            }

            return null;
        }
    }
}
