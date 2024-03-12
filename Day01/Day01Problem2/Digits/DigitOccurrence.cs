namespace Day01Problem2.Digits
{
    public class DigitOccurrence
    {
        public char DigitChar { get; private init; }
        public int Index { get; private init; }

        public DigitOccurrence(char digitChar, int index)
        {
            DigitChar = digitChar;
            Index = index;
        }

        public int DigitValue
        {
            get
            {
                return DigitChar - '0';
            }
        }
    }
}
