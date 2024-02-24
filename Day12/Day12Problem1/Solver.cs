namespace Day12Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            long totalWays = 0;
            for (int i = 0; i < inputLines.Length; i++)
            {
                string line = inputLines[i];
                string[] halves = line.Split(' ');
                string[] segmentArr = halves[0].Split('.', StringSplitOptions.RemoveEmptyEntries).ToArray();
                List<List<string>> segmentArrangements = new List<List<string>>();
                foreach (string segment in segmentArr)
                {
                    var segmentWays = new List<string>();
                    GetSegmentWays(segment, ref segmentWays);
                    segmentArrangements.Add(segmentWays);
                }

                long result = 0;
                GetWayCombinations(segmentArrangements, 0, string.Empty, halves[1], ref result);
                totalWays += result;
                Console.Out.WriteLine($"{(double)i / inputLines.Length * 100} percent complete...");
            }

            return totalWays;
        }

        private static string GetSegmentWayCount(string input)
        {
            var result = new List<int>();
            int damagedCount = 0;
            foreach (char c in input)
            {
                if (c == '#') damagedCount++;
                else
                {
                    if (damagedCount > 0)
                    {
                        result.Add(damagedCount);
                        damagedCount = 0;
                    }
                }
            }

            if (damagedCount > 0)
            {
                result.Add(damagedCount);
            }

            return string.Join(",", result);
        }

        private static void GetSegmentWays(string input, ref List<string> result)
        {
            if (!input.Any(c => c == '?'))
            {
                result.Add(GetSegmentWayCount(input));
            }
            else
            {
                int firstWildcard = input.IndexOf('?');
                GetSegmentWays(input.Substring(0, firstWildcard) + '#' + input.Substring(firstWildcard + 1), ref result);
                GetSegmentWays(input.Substring(0, firstWildcard) + '.' + input.Substring(firstWildcard + 1), ref result);
            }
        }

        private static void GetWayCombinations(List<List<string>> segmentWays, int index, string current, string target, ref long ways)
        {
            if (index == segmentWays.Count)
            {
                if (string.Equals(current, target))
                {
                    ways++;
                }
            }
            else
            {
                for (int i = 0; i < segmentWays[index].Count; i++)
                {
                    string next = segmentWays[index][i];
                    string separator;
                    if (string.IsNullOrEmpty(current) || string.IsNullOrEmpty(next))
                    {
                        separator = string.Empty;
                    }
                    else
                    {
                        separator = ",";
                    }

                    GetWayCombinations(segmentWays, index + 1, current + separator + next, target, ref ways);
                }
            }
        }
    }
}
