using System.Collections.Immutable;

namespace Day02Problem2
{
    public class Draw
    {
        public ImmutableList<DrawnColor> DrawnColors { get; private init; }

        private Draw() 
        {
            throw new InvalidOperationException($"Use the static {nameof(Parse)} method to create instances of {nameof(Draw)}");
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
