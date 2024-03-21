using Day01Problem2.Digits;
using Day01Problem2.InputLines;
using Day01Problem2.NumberWords;
using Moq;

namespace Day01Problem2.UnitTests.InputLines
{
    [TestClass]
    public class InputLineTests
    {
        private readonly Mock<IDigitSearchFactory> _digitSearchFactoryMock = new Mock<IDigitSearchFactory>();
        private readonly Mock<IDigitSearch> _digitSearchMock = new Mock<IDigitSearch>();
        private readonly Mock<INumberWordSearchFactory> _numberWordSearchFactoryMock = new Mock<INumberWordSearchFactory>();
        private readonly Mock<INumberWordSearch> _numberWordSearchMock = new Mock<INumberWordSearch>();
        private readonly string _input = "asdf1234";

        [TestInitialize]
        public void Setup()
        {
            _digitSearchFactoryMock.Reset();
            _digitSearchMock.Reset();
            _digitSearchFactoryMock.Setup(m => m.Create(_input)).Returns(_digitSearchMock.Object);
            _numberWordSearchFactoryMock.Reset();
            _numberWordSearchMock.Reset();
            _numberWordSearchFactoryMock.Setup(m => m.Create(_input)).Returns(_numberWordSearchMock.Object);
        }

        [TestMethod]
        public void InputLine_ProducesCorrectLineValue_WhenDigitFirstAndNumberWordLast()
        {
            _digitSearchMock.Setup(m => m.FirstDigitOccurrence).Returns(new DigitOccurrence('1', 0));
            _digitSearchMock.Setup(m => m.LastDigitOccurrence).Returns(new DigitOccurrence('4', 7));
            _numberWordSearchMock.Setup(m => m.FirstOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[0], 4));
            _numberWordSearchMock.Setup(m => m.LastOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[5], 9));

            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            int result = inputLine.GetLineValue();

            Assert.AreEqual(16, result);
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchMock.Verify(m => m.FirstDigitOccurrence, Times.Once());
            _digitSearchMock.Verify(m => m.LastDigitOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.FirstOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.LastOccurrence, Times.Once());
        }

        [TestMethod]
        public void InputLine_ProducesCorrectLineValue_WhenDigitFirstAndLast()
        {
            _digitSearchMock.Setup(m => m.FirstDigitOccurrence).Returns(new DigitOccurrence('1', 0));
            _digitSearchMock.Setup(m => m.LastDigitOccurrence).Returns(new DigitOccurrence('4', 17));
            _numberWordSearchMock.Setup(m => m.FirstOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[0], 4));
            _numberWordSearchMock.Setup(m => m.LastOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[5], 9));

            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            int result = inputLine.GetLineValue();

            Assert.AreEqual(14, result);
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchMock.Verify(m => m.FirstDigitOccurrence, Times.Once());
            _digitSearchMock.Verify(m => m.LastDigitOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.FirstOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.LastOccurrence, Times.Once());
        }

        [TestMethod]
        public void InputLine_ProducesCorrectLineValue_WhenNoNumberWords()
        {
            _digitSearchMock.Setup(m => m.FirstDigitOccurrence).Returns(new DigitOccurrence('1', 0));
            _digitSearchMock.Setup(m => m.LastDigitOccurrence).Returns(new DigitOccurrence('4', 17));

            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            int result = inputLine.GetLineValue();

            Assert.AreEqual(14, result);
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchMock.Verify(m => m.FirstDigitOccurrence, Times.Once());
            _digitSearchMock.Verify(m => m.LastDigitOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.FirstOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.LastOccurrence, Times.Once());
        }

        [TestMethod]
        public void InputLine_ProducesCorrectLineValue_WhenNumberWordFirstAndDigitLast()
        {
            _digitSearchMock.Setup(m => m.FirstDigitOccurrence).Returns(new DigitOccurrence('1', 4));
            _digitSearchMock.Setup(m => m.LastDigitOccurrence).Returns(new DigitOccurrence('4', 17));
            _numberWordSearchMock.Setup(m => m.FirstOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[0], 0));
            _numberWordSearchMock.Setup(m => m.LastOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[5], 9));

            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            int result = inputLine.GetLineValue();

            Assert.AreEqual(14, result);
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchMock.Verify(m => m.FirstDigitOccurrence, Times.Once());
            _digitSearchMock.Verify(m => m.LastDigitOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.FirstOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.LastOccurrence, Times.Once());
        }

        [TestMethod]
        public void InputLine_ProducesCorrectLineValue_WhenNumberWordFirstAndLast()
        {
            _digitSearchMock.Setup(m => m.FirstDigitOccurrence).Returns(new DigitOccurrence('1', 4));
            _digitSearchMock.Setup(m => m.LastDigitOccurrence).Returns(new DigitOccurrence('4', 7));
            _numberWordSearchMock.Setup(m => m.FirstOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[0], 0));
            _numberWordSearchMock.Setup(m => m.LastOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[5], 9));

            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            int result = inputLine.GetLineValue();

            Assert.AreEqual(16, result);
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchMock.Verify(m => m.FirstDigitOccurrence, Times.Once());
            _digitSearchMock.Verify(m => m.LastDigitOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.FirstOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.LastOccurrence, Times.Once());
        }

        [TestMethod]
        public void InputLine_ProducesCorrectLineValue_WhenNoDigits()
        {
            _numberWordSearchMock.Setup(m => m.FirstOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[0], 0));
            _numberWordSearchMock.Setup(m => m.LastOccurrence).Returns(new NumberWordOccurrence(NumberWord.AllNumberWords[5], 9));

            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            int result = inputLine.GetLineValue();

            Assert.AreEqual(16, result);
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchMock.Verify(m => m.FirstDigitOccurrence, Times.Once());
            _digitSearchMock.Verify(m => m.LastDigitOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.FirstOccurrence, Times.Once());
            _numberWordSearchMock.Verify(m => m.LastOccurrence, Times.Once());
        }

        [TestMethod]
        public void InputLine_ShouldThrowIfNoDigitsOrWords()
        {
            var inputLine = new InputLine(_input, _digitSearchFactoryMock.Object, _numberWordSearchFactoryMock.Object);
            Assert.ThrowsException<InvalidOperationException>(() => inputLine.GetLineValue());
        }

    }
}
