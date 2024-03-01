using System.Collections.Concurrent;

namespace Day17Problem2
{
    public class HeatLossStats
    {
        public int[][] StepHeatLoss { get; init; }
        public ConcurrentDictionary<string, long>[][] TotalHeatLoss { get; init; }

        public HeatLossStats(string[] inputLines)
        {
            StepHeatLoss = new int[inputLines.Length][];
            TotalHeatLoss = new ConcurrentDictionary<string, long>[inputLines.Length][];
            for (int i = 0; i < inputLines.Length; i++)
            {
                StepHeatLoss[i] = new int[inputLines[i].Length];
                TotalHeatLoss[i] = new ConcurrentDictionary<string, long>[inputLines[0].Length];
                for (int j = 0; j < inputLines.Length; j++)
                {
                    StepHeatLoss[i][j] = inputLines[i][j] - '0';
                    TotalHeatLoss[i][j] = new ConcurrentDictionary<string, long>();
                }
            }
        }
    }
}
