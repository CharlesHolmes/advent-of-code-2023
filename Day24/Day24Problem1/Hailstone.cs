namespace Day24Problem1
{
    public class Hailstone
    {
        public long X0 { get; init; }
        public long Y0 { get; init; }

        public int VX { get; init; }
        public int VY { get; init; }

        public decimal Slope
        {
            get
            {
                return (decimal)VY / VX;
            }
        }

        public decimal Intercept
        {
            get
            {
                return Y0 - (X0 * Slope);
            }
        }

        public bool PathIntersects(Hailstone other, long min, long max)
        {
            if (Slope == other.Slope)
            {
                return Intercept == other.Intercept;
            }

            decimal xInterceptCoord = (other.Intercept - Intercept) / (Slope - other.Slope);
            decimal yInterceptCoord = xInterceptCoord * Slope + Intercept;
            decimal timeOfIntercept = (xInterceptCoord - X0) / VX;
            decimal otherTimeOfIntercept = (xInterceptCoord - other.X0) / other.VX;

            return xInterceptCoord >= min
                && xInterceptCoord <= max
                && yInterceptCoord >= min
                && yInterceptCoord <= max
                && timeOfIntercept >= 0
                && otherTimeOfIntercept >= 0;
        }
    }
}
