using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int GetDigitValue(char digit)
        {
            return digit - '0';
        }

        private int GetFirstNumberWordIndex()
        {

        }

        private int GetFirstDigitIndex()
        {

        }

        private int GetLastNumberWordIndex()
        {

        }

        private int GetLastDigitIndex()
        {

        }
    }
}
