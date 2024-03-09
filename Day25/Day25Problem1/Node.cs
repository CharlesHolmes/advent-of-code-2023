namespace Day25Problem1
{
    public class Node
    {
        public string Name { get; init; }
        public List<Edge> Edges { get; } = new List<Edge>();
        public override string ToString()
        {
            return Name;
        }
    }
}
