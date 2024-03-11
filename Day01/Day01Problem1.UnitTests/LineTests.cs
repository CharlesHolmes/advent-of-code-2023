namespace Day01Problem1.UnitTests
{
    [TestClass]
    public class LineTests
    {
        [DataTestMethod]
        [DataRow("asd5blwpol1", 51, DisplayName = "Exactly two numbers")]
        [DataRow("8fweue62jre874iujer", 84, DisplayName = "More than two numbers")]
        [DataRow("iufoqjfjd4lkdkogfs", 44, DisplayName = "Only one number")]
        public void TestInputSingleLine(string inputLine, long expected)
        {
            long actual = new Line(inputLine).GetLineValue();
            Assert.AreEqual(expected, actual);
        }
    }
}