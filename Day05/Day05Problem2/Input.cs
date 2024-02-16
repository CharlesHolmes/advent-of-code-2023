using System.Collections.Immutable;

namespace Day05Problem2
{
    public partial class Solver
    {
        public class Input
        {
            public ImmutableList<SeedRange> Seeds { get; private set; }
            public ImmutableDictionary<string, Map> Maps { get; private set; }

            private Input()
            {
                Seeds = ImmutableList<SeedRange>.Empty;
                Maps = ImmutableDictionary<string, Map>.Empty;
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

                var allMapsBuilder = ImmutableDictionary.CreateBuilder<string, Map>();
                var mapBuilder = Map.CreateBuilder();
                foreach (string line in inputLines.Skip(2))
                {
                    if (line.Length == 0)
                    {
                        var built = mapBuilder.Build();
                        allMapsBuilder.Add(built.SourceName, built);
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
                allMapsBuilder.Add(map.SourceName, map);
                input.Maps = allMapsBuilder.ToImmutable();
                return input;
            }
        }
    }
}
