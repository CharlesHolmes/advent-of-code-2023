using Day01Problem2.NumberWords;

namespace Day01Problem2.UnitTests.NumberWords
{
    [TestClass]
    public class NumberWordOccurrenceTests
    {
        [TestMethod]
        public void NumberWordOccurrence_WhenCreatedWithNumberWordAndIndex_ThenPropertiesAreSet()
        {
            // Arrange
            var numberWord = NumberWord.AllNumberWords[0];
            var index = 1;

            // Act
            var numberWordOccurrence = new NumberWordOccurrence(numberWord, index);

            // Assert
            Assert.AreEqual(numberWord, numberWordOccurrence.NumberWord);
            Assert.AreEqual(index, numberWordOccurrence.Index);
        }
    }
}
