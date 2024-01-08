namespace Day02Problem1
{
    public class Draw
    {
        private readonly List<DrawnColor> _drawnColors = new List<DrawnColor>();

        private Draw() { }

        public static Draw Parse(string drawString)
        {
            var result = new Draw();
            result._drawnColors.AddRange(drawString.Split(',').Select(s => DrawnColor.Parse(s)));
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
