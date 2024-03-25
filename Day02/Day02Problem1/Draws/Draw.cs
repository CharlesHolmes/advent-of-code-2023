using System.Collections.Immutable;
using Day02Problem1.DrawnColors;

namespace Day02Problem1.Draws
{
    public class Draw : IDraw
    {
        private ImmutableList<IDrawnColor> _drawnColors { get; init; }

        public Draw(
            IDrawnColorFactory _drawnColorFactory,
            string drawString)
        {
            _drawnColors = ImmutableList.CreateRange(drawString.Split(',').Select(s => _drawnColorFactory.Create(s)));
        }

        public bool IsPossibleGivenMaxColorCounts(Dictionary<string, int> maxColorCounts)
        {
            foreach (IDrawnColor drawnColor in _drawnColors)
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
