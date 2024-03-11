namespace Day01Problem1.UnitTests
{
    [TestClass]
    public class LineFactoryTests
    {
        [TestMethod]
        public void TestInputSingleLine()
        {
            string inputLine = "a1b4c";
            var factory = new LineFactory();
            var line = factory.Create(inputLine);
            Assert.AreEqual(new Line(inputLine).GetLineValue(), line.GetLineValue());
        }
    }
}