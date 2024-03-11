using Moq;

namespace Day01Problem1.UnitTests
{
    [TestClass]
    public class SolverTests
    {
        private readonly Mock<ILine> _lineMock = new Mock<ILine>();
        private readonly Mock<ILineFactory> _lineFactoryMock = new Mock<ILineFactory>();

        [TestInitialize()]
        public void Setup()
        {
            _lineFactoryMock.Reset();
        }

        [TestMethod]
        public void TestInputSingleLine()
        {
            string inputLine = "abc";
            long expected = 123;
            _lineMock.Setup(m => m.GetLineValue()).Returns(expected);
            _lineFactoryMock.Setup(m => m.Create(inputLine)).Returns(_lineMock.Object);
            var solver = new Solver(_lineFactoryMock.Object);
            long actual = solver.GetSolution([inputLine]);
            _lineFactoryMock.Verify(m => m.Create(inputLine));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInputMultipleLines()
        {
            string[] input = ["abc", "def", "ghi"];
            long expected = 30;
            _lineMock.Setup(m => m.GetLineValue()).Returns(expected / input.Length);
            _lineFactoryMock.Setup(m => m.Create(It.IsIn(input))).Returns(_lineMock.Object);
            var solver = new Solver(_lineFactoryMock.Object);
            long actual = solver.GetSolution(input);
            foreach (string s in input)
            {
                _lineFactoryMock.Verify(m => m.Create(s));
            }

            Assert.AreEqual(30, actual);
        }
    }
}