namespace Day25Problem1
{
    public class Edge
    {
        public Node Source { get; init; }
        public Node Destination { get; init; }
        public Edge TwinEdge { get; set; }
        public override string ToString()
        {
            return $"{Source}->{Destination}";
        }
    }
}
