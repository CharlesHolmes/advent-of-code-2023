namespace Day02Problem2
{
    public class DrawnColor
    {
        public string CubeColor { get; private init; }
        public int CubeCount { get; private init; }

        private DrawnColor()
        {
            throw new InvalidOperationException($"Use the static {nameof(Parse)} method to create instances of {nameof(DrawnColor)}");
        }

        public static DrawnColor Parse(string drawnColorString)
        {
            string[] parts = drawnColorString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new DrawnColor
            {
                CubeColor = parts[1],
                CubeCount = int.Parse(parts[0])
            };
        }
    }
}
