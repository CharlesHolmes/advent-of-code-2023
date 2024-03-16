using Day01Problem2.NumberWords;
using FluentAssertions;

namespace Day01Problem2.UnitTests.NumberWords
{
    [TestClass]
    public class NumberWordTests
    {
        [TestMethod]
        public void NumberWord_ShouldBeCorrectType()
        {
            NumberWord.AllNumberWords.Should().AllBeOfType<NumberWord>();
        }

        [TestMethod]
        public void NumberWord_AllNumberWords_ShouldContainOneThroughNine()
        {
            NumberWord.AllNumberWords.Select(w => new { w.Word, w.Value })
                .Should()
                .BeEquivalentTo(new[]
                {
                    new { Word = "one", Value = 1 },
                    new { Word = "two", Value = 2 },
                    new { Word = "three", Value = 3 },
                    new { Word = "four", Value = 4 },
                    new { Word = "five", Value = 5 },
                    new { Word = "six", Value = 6 },
                    new { Word = "seven", Value = 7 },
                    new { Word = "eight", Value = 8 },
                    new { Word = "nine", Value = 9 }
                });
        }
    }
}
