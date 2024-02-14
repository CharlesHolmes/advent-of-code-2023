namespace Day05Problem1
{
    public partial class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var input = Input.ParseInput(inputLines);
            long lowestLocation = long.MaxValue;
            foreach (long seed in input.Seeds)
            {
                long number = seed;
                string location = "seed";
                while (location != "location")
                {
                    var map = input.Maps[location];
                    foreach (var range in map.Ranges)
                    {
                        if (number >= range.SourceStart && number <= (range.SourceStart + range.Length))
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
    }
}
