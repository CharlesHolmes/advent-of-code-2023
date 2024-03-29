﻿namespace Day06Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            int[] timeArr = inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => int.Parse(x)).ToArray();
            int[] distanceRecordArr = inputLines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => int.Parse(x)).ToArray();
            List<Race> raceList = new List<Race>();
            for (int i = 0; i < timeArr.Length; i++)
            {
                raceList.Add(new Race
                {
                    Time = timeArr[i],
                    DistanceRecord = distanceRecordArr[i]
                });
            }

            long waysToWinProduct = 1;
            foreach (Race race in raceList)
            {
                int waysToWin = 0;
                for (int windupTime = 0; windupTime < race.Time; windupTime++)
                {
                    int raceTime = race.Time - windupTime;
                    int distance = windupTime * raceTime;
                    if (distance > race.DistanceRecord) waysToWin++;
                }

                waysToWinProduct *= waysToWin;
            }

            return waysToWinProduct;
        }
    }
}
