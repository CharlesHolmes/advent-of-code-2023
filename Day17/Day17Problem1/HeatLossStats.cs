namespace Day17Problem1
{
    public class HeatLossStats
    {
        public int[][] StepHeatLoss { get; init; }
        public long[][][][][] TotalHeatLoss { get; init; }

        public HeatLossStats(string[] inputLines)
        {
            StepHeatLoss = new int[inputLines.Length][];
            TotalHeatLoss = new long[inputLines.Length][][][][];
            for (int i = 0; i < inputLines.Length; i++)
            {
                StepHeatLoss[i] = new int[inputLines[i].Length];
                TotalHeatLoss[i] = new long[inputLines[i].Length][][][];
                for (int j = 0; j < inputLines.Length; j++)
                {
                    StepHeatLoss[i][j] = inputLines[i][j] - '0';
                    TotalHeatLoss[i][j] = new long[Enum.GetValues<Direction>().Count()][][];
                    foreach (Direction d1 in Enum.GetValues<Direction>())
                    {
                        TotalHeatLoss[i][j][(int)d1] = new long[Enum.GetValues<Direction>().Count()][];
                        foreach (Direction d2 in Enum.GetValues<Direction>())
                        {
                            TotalHeatLoss[i][j][(int)d1][(int)d2] = new long[Enum.GetValues<Direction>().Count()];
                            foreach (Direction d3 in Enum.GetValues<Direction>())
                            {
                                TotalHeatLoss[i][j][(int)d1][(int)d2][(int)d3] = long.MaxValue;
                            }
                        }
                    }
                }
            }
        }
    }
}
