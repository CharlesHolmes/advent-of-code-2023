using Day01Problem2.Digits;

namespace Day01Problem2.UnitTests.Digits
{
    [TestClass]
    public class DigitSearchTests
    {
        [DataTestMethod]
        [DataRow("ab3cdl8998a", '3', 2, 3)]
        [DataRow("12kdfsao0981la", '1', 0, 1)]
        [DataRow("fdsaslfdl0", '0', 9, 0)]
        public void VerifyFirstDigitIdentified(string input, char c, int index, int value)
        {
            var search = new DigitSearch(input);
            Assert.IsNotNull(search.FirstDigitOccurrence);
            Assert.AreEqual(c, search.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(index, search.FirstDigitOccurrence.Index);
            Assert.AreEqual(value, search.FirstDigitOccurrence.DigitValue);
        }

        [TestMethod]
        public void VerifyFirstDigitNull()
        {
            var search = new DigitSearch("abcdefg");
            Assert.IsNull(search.FirstDigitOccurrence);
        }

        [DataTestMethod]
        [DataRow("ab3cdl8998a", '8', 9, 8)]
        [DataRow("12kdfsao0981la", '1', 11, 1)]
        [DataRow("fdsaslfdl0", '0', 9, 0)]
        public void VerifyLastDigitIdentified(string input, char c, int index, int value)
        {
            var search = new DigitSearch(input);
            Assert.IsNotNull(search.LastDigitOccurrence);
            Assert.AreEqual(c, search.LastDigitOccurrence.DigitChar);
            Assert.AreEqual(index, search.LastDigitOccurrence.Index);
            Assert.AreEqual(value, search.LastDigitOccurrence.DigitValue);
        }

        [TestMethod]
        public void VerifyLastDigitNull()
        {
            var search = new DigitSearch("abcdefg");
            Assert.IsNull(search.LastDigitOccurrence);
        }
    }
}
