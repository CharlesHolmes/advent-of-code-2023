using Day01Problem2.InputLines;
using Moq;

namespace Day01Problem2.UnitTests
{
    [TestClass]
    public class SolverTests
    {
        private readonly Mock<IInputLineFactory> _inputLineFactoryMock = new Mock<IInputLineFactory>();

        [TestInitialize()]
        public void Setup()
        {
            _inputLineFactoryMock.Reset();
        }

        [TestMethod]
        public void TestInputSingleLine()
        {
            int expected = 123;
            string inputLine = "abc";
            var lineMock = new Mock<IInputLine>();
            lineMock.Setup(m => m.GetLineValue()).Returns(expected);
            _inputLineFactoryMock.Setup(m => m.Create(inputLine)).Returns(lineMock.Object);
            var solver = new Solver(_inputLineFactoryMock.Object);
            long actual = solver.GetSolution([inputLine]);
            _inputLineFactoryMock.Verify(m => m.Create(inputLine));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInputMultipleLines()
        {
            string[] input = ["abc", "def", "ghi"];
            int expected = 30;
            var lineMock = new Mock<IInputLine>();
            lineMock.Setup(m => m.GetLineValue()).Returns(expected / input.Length);
            _inputLineFactoryMock.Setup(m => m.Create(It.IsIn(input))).Returns(lineMock.Object);
            var solver = new Solver(_inputLineFactoryMock.Object);
            long actual = solver.GetSolution(input);
            foreach (string s in input)
            {
                _inputLineFactoryMock.Verify(m => m.Create(s));
            }

            Assert.AreEqual(30, actual);
        }
    }
}