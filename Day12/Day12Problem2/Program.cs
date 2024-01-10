using System.Collections.Concurrent;

namespace Day12Problem2
{
    internal class Program
    {
        private static readonly ConcurrentDictionary<string, ulong> _cachedWays = new ConcurrentDictionary<string, ulong>();

        private static ulong GetSpringWaysCount(string springs, List<int> groupings)
        {
            string cacheKey = $"{springs}/{string.Join(',', groupings)}";
            if (_cachedWays.TryGetValue(cacheKey, out ulong cachedResult))
            {
                return cachedResult;
            }

            ulong result;
            if (!groupings.Any())
            {
                if (!springs.Any(c => c == '#'))
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }
            }
            else if (springs.Length == 0)
            {
                result = 0;
            }
            else
            {
                char nextChar = springs.First();
                int nextGrouping = groupings.First();
                if (nextChar == '#')
                {
                    result = HandleSpring(springs, groupings, nextGrouping);
                }
                else if (nextChar == '.')
                {
                    result = GetSpringWaysCount(springs.Substring(1), groupings);
                }
                else if (nextChar == '?')
                {
                    result = HandleSpring(springs, groupings, nextGrouping)
                        + GetSpringWaysCount(springs.Substring(1), groupings);
                }
                else
                {
                    throw new Exception("Should not have arrived here.");
                }
            }

            _cachedWays[cacheKey] = result;
            return result;
        }

        private static ulong HandleSpring(string springs, List<int> groupings, int nextGrouping)
        {
            if (nextGrouping > springs.Length)
            {
                return 0;
            }

            string potentialGroup = springs.Substring(0, nextGrouping);
            if (potentialGroup.Any(c => c != '#' && c != '?'))
            {
                return 0;
            }

            if (springs.Length == nextGrouping)
            {
                if (groupings.Count == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            if (springs[nextGrouping] == '?' || springs[nextGrouping] == '.')
            {
                return GetSpringWaysCount(springs.Substring(nextGrouping + 1), groupings.Skip(1).ToList());
            }
            else
            {
                return 0;
            }
        }

        static async Task Main(string[] args)
        {
            int linesDone = 0;
            ulong totalCount = 0;
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            Parallel.ForEach(inputLines, line =>
            {
                var halves = line.Split(' ');
                var originalSprings = halves[0];
                var multipliedSprings = string.Join('?', Enumerable.Repeat(originalSprings, 5));
                var originalGroupsString = halves[1];
                var multipliedGroupsString = string.Join(',', Enumerable.Repeat(originalGroupsString, 5));
                var multipliedGroups = multipliedGroupsString.Split(',').Select(int.Parse).ToList();
                var count = GetSpringWaysCount(multipliedSprings, multipliedGroups);
                var newTotalCount = Interlocked.Add(ref totalCount, count);
                var newLinesDone = Interlocked.Increment(ref linesDone);
                Console.Out.WriteLine($"{(double)newLinesDone / inputLines.Length * 100} percent complete.  {newTotalCount} ways identified.");
            });

            Console.Out.WriteLine(totalCount);
        }
    }
}
