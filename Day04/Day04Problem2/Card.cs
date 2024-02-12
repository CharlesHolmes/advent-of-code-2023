namespace Day04Problem2
{
    public class Card
    {
        public int Id { get; set; }
        public HashSet<int> WinningNumbers { get; } = new HashSet<int>();
        public List<int> CardNumbers { get; } = new List<int>();
        public int WinnerCount { get; set; }

        public static Card ParseCard(string cardLine)
        {
            var card = new Card();
            string[] idAndNumbers = cardLine.Split(':');
            string idHalf = idAndNumbers[0];
            string cardIdString = idHalf.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
            card.Id = int.Parse(cardIdString);
            string justNumbers = idAndNumbers[1];
            string[] numberHalves = justNumbers.Split('|');
            string allWinnerString = numberHalves[0];
            foreach (string winnerString in allWinnerString.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                card.WinningNumbers.Add(int.Parse(winnerString));
            }

            string allCardNumberString = numberHalves[1];
            foreach (string cardNumberString in allCardNumberString.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                card.CardNumbers.Add(int.Parse(cardNumberString));
            }

            int score = 0;
            foreach (int number in card.CardNumbers)
            {
                if (card.WinningNumbers.Contains(number))
                {
                    score++;
                }
            }

            card.WinnerCount = score;
            return card;
        }
    }
}
