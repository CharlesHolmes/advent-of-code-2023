namespace Day01Problem1.UnitTests
{
    [TestClass]
    public class FullInputTest
    {
        [TestMethod]
        public async Task TestRealInputIfExists()
        {
            var solver = new Solver(new LineFactory());
            string projectName = solver.GetType().Assembly.GetName().Name!;
            string inputFolderPath = Environment.ExpandEnvironmentVariables(
                $"%USERPROFILE%\\Desktop\\AdventOfCode2023Inputs\\{projectName}");
            string inputFilePath = $"{inputFolderPath}\\input.txt";
            string resultFilePath = $"{inputFolderPath}\\result.txt";
            if (File.Exists(inputFilePath) && File.Exists(resultFilePath))
            {
                string[] input = await File.ReadAllLinesAsync(inputFilePath);
                long expected = long.Parse((await File.ReadAllLinesAsync(resultFilePath))[0]);
                long actual = solver.GetSolution(input);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.Inconclusive("Unable to test due to missing input and/or result files.");
            }
        }
    }
}