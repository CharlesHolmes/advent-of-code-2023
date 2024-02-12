namespace Day04Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var cardDictionary = new Dictionary<int, Card>();
            foreach (string cardLine in inputLines)
            {
                var card = Card.ParseCard(cardLine);
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

            return totalCardCount;
        }
    }
}
