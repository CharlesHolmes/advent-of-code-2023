using Day01Problem2.Digits;

namespace Day01Problem2.UnitTests.Digits
{
    [TestClass]
    public class DigitOccurrenceTests
    {
        [DataTestMethod]
        [DataRow('0', 3, 0)]
        [DataRow('9', 5, 9)]
        [DataRow('1', 2, 1)]
        public void TestInputSingleLine(char character, int index, int expectedValue)
        {
            var occurrence = new DigitOccurrence(character, index);
            Assert.AreEqual(index, occurrence.Index);
            Assert.AreEqual(character, occurrence.DigitChar);
            Assert.AreEqual(expectedValue, occurrence.DigitValue);
        }
    }
}
