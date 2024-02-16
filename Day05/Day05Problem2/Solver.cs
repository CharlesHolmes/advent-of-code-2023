namespace Day05Problem2
{
    public partial class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var input = Input.ParseInput(inputLines);
            long lowestLocation = long.MaxValue;
            foreach (SeedRange seedRange in input.Seeds)
            {
                for (long seed = seedRange.Start; seed <= seedRange.End; seed++)
                {
                    long number = seed;
                    string location = "seed";
                    while (location != "location")
                    {
                        var map = input.Maps[location];
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
            }

            return lowestLocation;
        }
    }
}
