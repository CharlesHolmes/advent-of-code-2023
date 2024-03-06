namespace Day22Problem2
{
    public class Brick
    {
        public Point Point1 { get; private init; }
        public Point Point2 { get; private init; }

        public Brick(Point p1, Point p2)
        {
            Point1 = p1;
            Point2 = p2;
        }

        public Brick(Brick other)
        {
            Point1 = new Point(other.Point1);
            Point2 = new Point(other.Point2);
        }

        public bool IntersectsIfAtSameHeight(Brick other)
        {
            return Math.Min(Point1.X, Point2.X) <= Math.Max(other.Point1.X, other.Point2.X)
                && Math.Max(Point1.X, Point2.X) >= Math.Min(other.Point1.X, other.Point2.X)
                && Math.Max(Point1.Y, Point2.Y) >= Math.Min(other.Point1.Y, other.Point2.Y)
                && Math.Min(Point1.Y, Point2.Y) <= Math.Max(other.Point1.Y, other.Point2.Y);
        }
    }
}
