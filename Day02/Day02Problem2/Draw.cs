using System.Collections.Immutable;

namespace Day02Problem2
{
    public class Draw
    {
        public ImmutableList<DrawnColor> DrawnColors { get; private init; } = ImmutableList.Create<DrawnColor>();

        private Draw()
        {
        }

        public static Draw Parse(string drawString)
        {
            var result = new Draw
            {
                DrawnColors = ImmutableList.CreateRange(drawString.Split(',').Select(s => DrawnColor.Parse(s)))
            };

            return result;
        }
    }
}
