using System.Collections.Immutable;

namespace Day05Problem1
{
    public partial class Solver
    {
        public class Input
        {
            public ImmutableList<long> Seeds { get; private set; }
            public ImmutableDictionary<string, Map> Maps { get; private set; }

            private Input()
            {
                Seeds = ImmutableList<long>.Empty;
                Maps = ImmutableDictionary<string, Map>.Empty;
            }

            public static Input ParseInput(string[] inputLines)
            {
                var input = new Input();
                input.Seeds = ImmutableList.CreateRange(inputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => long.Parse(x)));
                var mapsByNameBuilder = ImmutableDictionary.CreateBuilder<string, Map>();
                var map = new Map();
                foreach (string line in inputLines.Skip(2))
                {
                    if (line.Length == 0)
                    {
                        mapsByNameBuilder.Add(map.SourceName, map);
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

                mapsByNameBuilder.Add(map.SourceName, map);
                input.Maps = mapsByNameBuilder.ToImmutable();
                return input;
            }
        }
    }
}
