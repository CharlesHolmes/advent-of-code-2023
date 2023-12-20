namespace Day04Problem2
{
    public class Card
    {
        public int Id;
        public readonly HashSet<int> WinningNumbers = new HashSet<int>();
        public readonly List<int> CardNumbers = new List<int>();
        public int WinnerCount;
    }

    internal class Program
    {
        private static Card ParseCard(string cardLine)
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
            var cardNumbers = new List<int>();
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

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var cardDictionary = new Dictionary<int, Card>();
            foreach (string cardLine in inputLines)
            {
                var card = ParseCard(cardLine);
                cardDictionary.Add(card.Id, card);
            }

            var toProcess = new Queue<Card>();
            foreach (Card card in cardDictionary.Values)
            {
                toProcess.Enqueue(card);
            }

            long totalCardCount = 0;
            while (toProcess.Any())
            {
                var current = toProcess.Dequeue();
                totalCardCount++;
                for (int i = 1; i <= current.WinnerCount; i++)
                {
                    toProcess.Enqueue(cardDictionary[current.Id + i]);
                }
            }

            Console.Out.WriteLine(totalCardCount);
        }
    }
}