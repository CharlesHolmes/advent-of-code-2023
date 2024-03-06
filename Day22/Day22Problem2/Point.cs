namespace Day22Problem2
{
    public class Point
    {
        public int X { get; private init; }
        public int Y { get; private init; }
        public int Z { get; set; }

        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(Point other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }
    }
}
