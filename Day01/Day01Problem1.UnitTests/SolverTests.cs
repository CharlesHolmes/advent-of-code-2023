namespace Day01Problem1.UnitTests
{
    [TestClass]
    public class SolverTests
    {
        private Solver _solver = new();

        [TestInitialize()]
        public void Setup()
        {
            _solver = new Solver();
        }

        [DataTestMethod]
        [DataRow("asd5blwpol1", 51, DisplayName = "Exactly two numbers")]
        [DataRow("8fweue62jre874iujer", 84, DisplayName = "More than two numbers")]
        [DataRow("iufoqjfjd4lkdkogfs", 44, DisplayName = "Only one number")]
        public void TestInputSingleLine(string inputLine, long expected)
        {
            long actual = _solver.GetSolution([inputLine]);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInputMultipleLines()
        {
            var input = new string[3];
            input[0] = "7fgdjfgiuewr236hrf8e"; // 78
            input[1] = "kfghu43873908erjk09iw"; // 49
            input[2] = "98wq0ejoikfgd908kjl34"; // 94
            long expected = 221; // sum of above
            long actual = _solver.GetSolution(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task TestRealInputIfExists()
        {
            string projectName = _solver.GetType().Assembly.GetName().Name!;
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