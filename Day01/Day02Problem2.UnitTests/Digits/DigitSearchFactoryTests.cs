using Day01Problem2.Digits;

namespace Day01Problem2.UnitTests.Digits
{
    [TestClass]
    public class DigitSearchFactoryTests
    {
        // Experimenting with using Copilot to write these

        [TestMethod]
        public void Create_ReturnsNotNull()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abc123def456";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ReturnsCorrectType()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abc123def456";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsInstanceOfType(result, typeof(IDigitSearch));
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abc123def456";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(3, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abc123def456";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('6', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(11, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenNoDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abcdef";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.FirstDigitOccurrence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenNoDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abcdef";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.LastDigitOccurence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenEmptyString()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.FirstDigitOccurrence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenEmptyString()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.LastDigitOccurence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "123456";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(0, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "123456";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('6', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(5, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyOneDigit()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "1";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(0, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyOneDigit()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "1";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('1', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(0, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyOneDigitAndNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "a1b";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(1, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyOneDigitAndNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "a1b";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('1', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(1, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abc";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.FirstDigitOccurrence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "abc";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.LastDigitOccurence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyWhitespace()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.FirstDigitOccurrence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyWhitespace()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.LastDigitOccurence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyWhitespaceAndNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " a ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.FirstDigitOccurrence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyWhitespaceAndNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " a ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNull(result.LastDigitOccurence);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyWhitespaceAndDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " 1 ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(1, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyWhitespaceAndDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " 1 ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('1', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(1, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyWhitespaceAndMultipleDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " 123 ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(1, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyWhitespaceAndMultipleDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " 123 ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('3', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(3, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyNonWhitespaceAndMultipleDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "123";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(0, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyNonWhitespaceAndMultipleDigits()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "123";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('3', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(2, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyNonWhitespaceAndMultipleDigitsAndNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "a123b";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(1, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyNonWhitespaceAndMultipleDigitsAndNonDigitCharacters()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = "a123b";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('3', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(3, result.LastDigitOccurence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectFirstDigitOccurrence_WhenOnlyNonWhitespaceAndMultipleDigitsAndNonDigitCharactersAndWhitespace()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " a123b ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.FirstDigitOccurrence);
            Assert.AreEqual('1', result.FirstDigitOccurrence.DigitChar);
            Assert.AreEqual(2, result.FirstDigitOccurrence.Index);
        }

        [TestMethod]
        public void Create_ReturnsCorrectLastDigitOccurrence_WhenOnlyNonWhitespaceAndMultipleDigitsAndNonDigitCharactersAndWhitespace()
        {
            // Arrange
            var factory = new DigitSearchFactory();
            var inputString = " a123b ";
            // Act
            var result = factory.Create(inputString);
            // Assert
            Assert.IsNotNull(result.LastDigitOccurence);
            Assert.AreEqual('3', result.LastDigitOccurence.DigitChar);
            Assert.AreEqual(4, result.LastDigitOccurence.Index);
        }
    }
}
