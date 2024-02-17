namespace Day06Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            long time = long.Parse(string.Join("", inputLines[0].Split(':')[1].Where(char.IsDigit)));
            long distanceRecord = long.Parse(string.Join("", inputLines[1].Split(':')[1].Where(char.IsDigit)));

            long lowerWinningBound = 0;
            for (long windupTime = 0; windupTime < time; windupTime++)
            {
                long raceTime = time - windupTime;
                long distance = windupTime * raceTime;
                if (distance > distanceRecord)
                {
                    lowerWinningBound = windupTime;
                    break;
                }
            }

            long upperWinningBound = 0;
            for (long windupTime = time; windupTime >= 0; windupTime--)
            {
                long raceTime = time - windupTime;
                long distance = windupTime * raceTime;
                if (distance > distanceRecord)
                {
                    upperWinningBound = windupTime;
                    break;
                }
            }

            long waysToWin = upperWinningBound - lowerWinningBound + 1;
            return waysToWin;
        }
    }
}
