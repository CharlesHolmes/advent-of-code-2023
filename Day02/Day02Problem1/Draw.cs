using System.Collections.Immutable;

namespace Day02Problem1
{
    public class Draw
    {
        private ImmutableList<DrawnColor> _drawnColors { get; init; } = ImmutableList.Create<DrawnColor>();

        private Draw()
        {
        }

        public static Draw Parse(string drawString)
        {
            var result = new Draw
            {
                _drawnColors = ImmutableList.CreateRange(drawString.Split(',').Select(s => DrawnColor.Parse(s)))
            };
            return result;
        }

        public bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts)
        {
            foreach (DrawnColor drawnColor in _drawnColors)
            {
                if (!drawnColor.IsPossibleGivenMaxColorCounts(maxColorCounts))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
