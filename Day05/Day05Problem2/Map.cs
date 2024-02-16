using System.Collections.Immutable;

namespace Day05Problem2
{
    public partial class Solver
    {
        public class Map
        {
            public string SourceName { get; private set; } = string.Empty;
            public string DestName { get; private set; } = string.Empty;
            public ImmutableList<SourceDestRange> Ranges { get; private set; } = ImmutableList<SourceDestRange>.Empty;

            public static Builder CreateBuilder()
            {
                return new Builder();
            }

            public class Builder
            {
                private readonly Map _map;
                private readonly ImmutableList<SourceDestRange>.Builder _rangesBuilder;

                public Builder()
                {
                    _map = new Map();
                    _rangesBuilder = ImmutableList.CreateBuilder<SourceDestRange>();
                }

                public void SetSourceName(string sourceName)
                {
                    _map.SourceName = sourceName;
                }

                public void SetDestName(string destName)
                {
                    _map.DestName = destName;
                }

                public void AddRange(SourceDestRange range)
                {
                    _rangesBuilder.Add(range);
                }

                public Map Build()
                {
                    _map.Ranges = _rangesBuilder.ToImmutable();
                    return _map;
                }
            }
        }
    }
}
