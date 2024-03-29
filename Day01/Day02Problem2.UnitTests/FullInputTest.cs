using Day01Problem2.Digits;
using Day01Problem2.InputLines;
using Day01Problem2.NumberWords;

namespace Day01Problem2.UnitTests
{
    [TestClass]
    public class FullInputTest
    {
        private Solver? _solver;

        [TestInitialize()]
        public void Setup()
        {
            _solver = new Solver(
                new InputLineFactory(
                    new NumberWordSearchFactory(), new DigitSearchFactory()));
        }

        [TestMethod]
        public async Task TestRealInputIfExists()
        {
            string projectName = _solver!.GetType().Assembly.GetName().Name!;
            string inputFolderPath = Environment.ExpandEnvironmentVariables(
                $"%USERPROFILE%\\Desktop\\AdventOfCode2023Inputs\\{projectName}");
            string inputFilePath = $"{inputFolderPath}\\input.txt";
            string resultFilePath = $"{inputFolderPath}\\result.txt";
            if (File.Exists(inputFilePath) && File.Exists(resultFilePath))
            {
                string[] input = await File.ReadAllLinesAsync(inputFilePath);
                long expected = long.Parse((await File.ReadAllLinesAsync(resultFilePath))[0]);
                long actual = _solver.GetSolution(input);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.Inconclusive("Unable to test due to missing input and/or result files.");
            }
        }
    }
}