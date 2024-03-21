using Day01Problem2.Digits;
using Day01Problem2.InputLines;
using Day01Problem2.NumberWords;
using FluentAssertions;
using Moq;

namespace Day01Problem2.UnitTests.InputLines
{
    [TestClass]
    public class InputLineFactoryTests
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
        public void InputLineFactory_ShouldUseSearchFactories()
        {
            var factory = new InputLineFactory(_numberWordSearchFactoryMock.Object, _digitSearchFactoryMock.Object);
            factory.Create(_input);
            _numberWordSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
            _digitSearchFactoryMock.Verify(m => m.Create(_input), Times.Once());
        }

                [TestMethod]
        public void InputLineFactory_ShouldCreateCorrectType()
        {
            var factory = new InputLineFactory(_numberWordSearchFactoryMock.Object, _digitSearchFactoryMock.Object);
            var result = factory.Create(_input);
            result.Should().BeAssignableTo<IInputLine>();
        }
    }
}
