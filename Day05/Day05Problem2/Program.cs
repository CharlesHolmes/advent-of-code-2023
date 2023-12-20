namespace Day05Problem2
{
    public class SourceDestRange
    {
        public long SourceStart { get; set; }
        public long DestStart { get; set; }
        public long Length { get; set; }
    }

    public class Map
    {
        public string SourceName { get; set; } = string.Empty;
        public string DestName { get; set; } = string.Empty;
        public List<SourceDestRange> Ranges { get; } = new List<SourceDestRange>();
    }

    public class SeedRange
    {
        public long Start { get; set; }
        public long End { get; set; }
    }

    public class Input
    {
        public List<SeedRange> Seeds { get; } = new List<SeedRange>();
        public Dictionary<string, Map> Maps { get; } = new Dictionary<string, Map>();
    }

    internal class Program
    {
        static Input ParseInput(string[] inputLines)
        {
            var input = new Input();
            long[] seedRanges = inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => long.Parse(x)).ToArray();
            for (int i = 0; i < seedRanges.Length; i+=2)
            {
                input.Seeds.Add(new SeedRange
                {
                    Start = seedRanges[i],
                    End = seedRanges[i] + seedRanges[i + 1] - 1
                });
            }

            var map = new Map();
            foreach (string line in inputLines.Skip(2))
            {
                if (line.Length == 0)
                {
                    input.Maps.Add(map.SourceName, map);
                    map = new Map();
                }
                else
                {
                    if (line.Contains("-to-"))
                    {
                        string[] sourceDestArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]
                            .Split("-to-", StringSplitOptions.RemoveEmptyEntries);
                        map.SourceName = sourceDestArr[0];
                        map.DestName = sourceDestArr[1];
                    }
                    else if (line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length == 3)
                    {
                        string[] numberStringArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        map.Ranges.Add(new SourceDestRange
                        {
                            DestStart = long.Parse(numberStringArr[0]),
                            SourceStart = long.Parse(numberStringArr[1]),
                            Length = long.Parse(numberStringArr[2])
                        });
                    }
                }
            }

            input.Maps.Add(map.SourceName, map);
            return input;
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var input = ParseInput(inputLines);
            long lowestLocation = long.MaxValue;
            foreach (SeedRange seedRange in input.Seeds)
            {
                for (long seed = seedRange.Start; seed <= seedRange.End; seed++) {
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

            Console.Out.WriteLine(lowestLocation);
        }
    }
}