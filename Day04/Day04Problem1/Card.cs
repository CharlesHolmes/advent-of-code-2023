namespace Day04Problem1
{
    public class Card
    {
        private readonly HashSet<int> _drawnNumbers = new();
        private readonly HashSet<int> _winningNumbers = new();

        private Card() { }

        public static Card Parse(string cardLine)
        {
            var card = new Card();
            string justNumbers = cardLine.Split(':')[1];
            string[] twoHalves = justNumbers.Split('|');
            string winningNumberString = twoHalves[0];
            card._winningNumbers.UnionWith(ParseNumbers(winningNumberString));
            string drawnNumberString = twoHalves[1];
            card._drawnNumbers.UnionWith(ParseNumbers(drawnNumberString));
            return card;
        }

        private static IEnumerable<int> ParseNumbers(string input) =>
            input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s));

        public long GetCardScore()
        {
            int matchCount = _drawnNumbers.Intersect(_winningNumbers).Count();
            if (matchCount == 0) return 0;
            else return (long)Math.Pow(2, matchCount - 1);
        }
    }
}
