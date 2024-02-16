namespace Day05Problem2
{
    public class Solver
    {
        private long GetLowestFinalLocationFromSeedNumbers(Input input, List<long> seedNumbers)
        {
            long lowestLocation = long.MaxValue;
            foreach (long seed in seedNumbers)
            {
                long number = seed;
                string location = "seed";
                while (location != "location")
                {
                    var map = input.MapsBySource[location];
                    foreach (var range in map.Ranges)
                    {
                        if (number >= range.SourceStart && number < (range.SourceStart + range.Length))
                        {
                            number = range.DestStart + (number - range.SourceStart);
                            break;
                        }
                    }

                    location = map.DestName;
                }

                lowestLocation = Math.Min(lowestLocation, number);
            }

            return lowestLocation;
        }

        private SourceDestRange? GetRangeMatchingDestValue(Map map, long destValue)
        {
            foreach (SourceDestRange range in map.Ranges)
            {
                if (range.DestStart <= destValue
                    && (range.DestStart + range.Length - 1) >= destValue)
                {
                    return range;
                }
            }

            return null;
        }

        private long? WorkBackwardToSeedNumber(Input input, string mapDestName, long mapDestValue)
        {
            Map currentMap = input.MapsByDestination[mapDestName];
            SourceDestRange? matchingRange = GetRangeMatchingDestValue(currentMap, mapDestValue);
            if (matchingRange == null) return null;
            long sourceValue = matchingRange.SourceStart + (mapDestValue - matchingRange.DestStart);
            while (currentMap.SourceName != "seed")
            {
                currentMap = input.MapsByDestination[currentMap.SourceName];
                matchingRange = GetRangeMatchingDestValue(currentMap, sourceValue);
                if (matchingRange == null) return null;
                sourceValue = matchingRange.SourceStart + (sourceValue - matchingRange.DestStart);
            }

            return sourceValue;
        }

        private List<long> GetAllInflectionPointsAsSeedNumbers(Input input)
        {
            var result = new List<long>();
            foreach (Map map in input.MapsByDestination.Values)
            {
                foreach (SourceDestRange range in map.Ranges)
                {
                    long? seedNumberFromRangeStart = WorkBackwardToSeedNumber(input, map.DestName, range.DestStart);
                    if (seedNumberFromRangeStart.HasValue) result.Add(seedNumberFromRangeStart.Value);
                    long? seedNumberFromRangeEnd = WorkBackwardToSeedNumber(input, map.DestName, range.DestStart + range.Length - 1);
                    if (seedNumberFromRangeEnd.HasValue) result.Add(seedNumberFromRangeEnd.Value);
                }
            }

            return result;
        }

        public long GetSolution(string[] inputLines)
        {
            var input = Input.ParseInput(inputLines);
            List<long> inflectionPoints = GetAllInflectionPointsAsSeedNumbers(input);
            List<long> validInflectionPoints = inflectionPoints
                .Where(point => input.Seeds.Exists(seedRange => seedRange.Start <= point && seedRange.End >= point))
                .Union(input.Seeds.Select(seedRange => seedRange.Start))
                .Union(input.Seeds.Select(seedRange => seedRange.End))
                .ToList();
            return GetLowestFinalLocationFromSeedNumbers(input, validInflectionPoints);
        }
    }
}
