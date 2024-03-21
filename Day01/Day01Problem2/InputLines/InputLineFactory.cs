using Day01Problem2.Digits;
using Day01Problem2.NumberWords;

namespace Day01Problem2.InputLines
{
    public class InputLineFactory : IInputLineFactory
    {
        private readonly INumberWordSearchFactory _numberWordSearchFactory;
        private readonly IDigitSearchFactory _digitSearchFactory;

        public InputLineFactory(
            INumberWordSearchFactory numberWordSearchFactory,
            IDigitSearchFactory digitSearchFactory)
        {
            _numberWordSearchFactory = numberWordSearchFactory;
            _digitSearchFactory = digitSearchFactory;
        }

        public IInputLine Create(string inputLine) => new InputLine(inputLine, _digitSearchFactory, _numberWordSearchFactory);
    }
}
