namespace Day01Problem2.Digits
{
    public class DigitSearch
    {
        private readonly string _inputString;

        public DigitSearch(string inputString)
        {
            _inputString = inputString;
        }

        public DigitOccurrence? GetFirstDigitOccurrence()
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

        public DigitOccurrence? GetLastDigitOccurrence()
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
