﻿namespace Day01Problem2
{
    public class DigitOccurrence
    {
        public char DigitChar { get; init; }
        public int Index { get; init; }
        public int DigitValue 
        { 
            get
            {
                return DigitChar - '0';
            } 
        }
    }
}
