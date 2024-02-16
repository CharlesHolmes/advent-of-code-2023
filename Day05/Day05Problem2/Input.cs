using System.Collections.Immutable;

namespace Day05Problem2
{
    public class Input
    {
        public ImmutableList<SeedRange> Seeds { get; private set; }
        public ImmutableDictionary<string, Map> MapsBySource { get; private set; }
        public ImmutableDictionary<string, Map> MapsByDestination { get; private set; }

        private Input()
        {
            Seeds = ImmutableList<SeedRange>.Empty;
            MapsBySource = ImmutableDictionary<string, Map>.Empty;
            MapsByDestination = ImmutableDictionary<string, Map>.Empty;
        }

        public static Input ParseInput(string[] inputLines)
        {
            var input = new Input();
            long[] seedRanges = inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => long.Parse(x)).ToArray();
            var seedRangesBuilder = ImmutableList.CreateBuilder<SeedRange>();
            for (int i = 0; i < seedRanges.Length; i += 2)
            {
                seedRangesBuilder.Add(new SeedRange
                {
                    Start = seedRanges[i],
                    End = seedRanges[i] + seedRanges[i + 1] - 1
                });
            }

            input.Seeds = seedRangesBuilder.ToImmutable();

            var allMapsBySourceBuilder = ImmutableDictionary.CreateBuilder<string, Map>();
            var allMapsByDestinationBuilder = ImmutableDictionary.CreateBuilder<string, Map>();
            var mapBuilder = Map.CreateBuilder();
            foreach (string line in inputLines.Skip(2))
            {
                if (line.Length == 0)
                {
                    var built = mapBuilder.Build();
                    allMapsBySourceBuilder.Add(built.SourceName, built);
                    allMapsByDestinationBuilder.Add(built.DestName, built);
                    mapBuilder = Map.CreateBuilder();
                }
                else
                {
                    if (line.Contains("-to-"))
                    {
                        string[] sourceDestArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]
                            .Split("-to-", StringSplitOptions.RemoveEmptyEntries);
                        mapBuilder.SetSourceName(sourceDestArr[0]);
                        mapBuilder.SetDestName(sourceDestArr[1]);
                    }
                    else if (line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length == 3)
                    {
                        string[] numberStringArr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        mapBuilder.AddRange(new SourceDestRange
                        {
                            DestStart = long.Parse(numberStringArr[0]),
                            SourceStart = long.Parse(numberStringArr[1]),
                            Length = long.Parse(numberStringArr[2])
                        });
                    }
                }
            }

            var map = mapBuilder.Build();
            allMapsBySourceBuilder.Add(map.SourceName, map);
            input.MapsBySource = allMapsBySourceBuilder.ToImmutable();
            allMapsByDestinationBuilder.Add(map.DestName, map);
            input.MapsByDestination = allMapsByDestinationBuilder.ToImmutable();
            return input;
        }
    }
}
