using System.Collections.Immutable;

namespace Day04Problem2
{
    public class Card
    {
        public int Id { get; private set; }
        public ImmutableHashSet<int> WinningNumbers { get; private set; }
        public ImmutableList<int> CardNumbers { get; private set; }
        public int WinnerCount { get; private set; }

        private Card()
        {
            WinningNumbers = ImmutableHashSet<int>.Empty;
            CardNumbers = ImmutableList<int>.Empty;
        }

        public static Card ParseCard(string cardLine)
        {
            var card = new Card();
            string[] idAndNumbers = cardLine.Split(':');
            string idHalf = idAndNumbers[0];
            string cardIdString = idHalf.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
            card.Id = int.Parse(cardIdString);
            string justNumbers = idAndNumbers[1];
            string[] numberHalves = justNumbers.Split('|');
            string allWinningNumbersString = numberHalves[0];
            card.WinningNumbers = ImmutableHashSet.CreateRange(allWinningNumbersString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(winnerString => int.Parse(winnerString)));
            string allCardNumbersString = numberHalves[1];
            card.CardNumbers = ImmutableList.CreateRange(allCardNumbersString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(cardNumberString => int.Parse(cardNumberString)));
            card.WinnerCount = card.CardNumbers.Count(cardNumber => card.WinningNumbers.Contains(cardNumber));
            return card;
        }
    }
}
