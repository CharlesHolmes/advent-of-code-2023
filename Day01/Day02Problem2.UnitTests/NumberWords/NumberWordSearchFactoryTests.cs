using Day01Problem2.NumberWords;
using FluentAssertions;

namespace Day01Problem2.UnitTests.NumberWords
{
    [TestClass]
    public class NumberWordSearchFactoryTests
    {
        [TestMethod]
        public void NumberWordSearchFactory_ProvidesCorrectType()
        {
            var input = "12one432twoabc";
            var search = new NumberWordSearchFactory().Create(input);
            search.Should().BeAssignableTo<INumberWordSearch>();
        }

        [TestMethod]
        public void NumberWordSearchFactory_ShouldInstantiateNumberWordSearch()
        {
            var input = "12one432twoabc";
            var search = new NumberWordSearchFactory().Create(input);
            search.Should().BeEquivalentTo(new NumberWordSearch(input));
        }
    }
}
