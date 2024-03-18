using Day01Problem2.NumberWords;

namespace Day01Problem2.UnitTests.NumberWords
{
    [TestClass]
    public class NumberWordSearchTests
    {
        [TestMethod]
        public void NumberWordSearch_ShouldReturnFirstWord()
        {
            var input = "12one432twoabc";
            var search = new NumberWordSearch(input);
            Assert.IsNotNull(search.FirstOccurrence);
            Assert.AreEqual("one", search.FirstOccurrence.NumberWord.Word);           
            Assert.AreEqual(1, search.FirstOccurrence.NumberWord.Value);
            Assert.AreEqual(2, search.FirstOccurrence.Index);
        }

        [TestMethod]
        public void NumberWordSearch_ShouldReturnLastWord()
        {
            var input = "12one432twoabc";
            var search = new NumberWordSearch(input);
            Assert.IsNotNull(search.LastOccurrence);
            Assert.AreEqual("two", search.LastOccurrence.NumberWord.Word);
            Assert.AreEqual(2, search.LastOccurrence.NumberWord.Value);
            Assert.AreEqual(8, search.LastOccurrence.Index);
        }

        [TestMethod]
        public void NumberWordSearch_ShouldReturnNullsIfNoWords()
        {
            var input = "12ond432twabc";
            var search = new NumberWordSearch(input);
            Assert.IsNull(search.FirstOccurrence);
            Assert.IsNull(search.LastOccurrence);
        }
    }
}
