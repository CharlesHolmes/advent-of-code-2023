namespace Day04Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var cardsById = inputLines
                .Select(line => Card.ParseCard(line))
                .ToDictionary(card => card.Id);

            var toProcess = new Queue<Card>(cardsById.Values);
            long totalCardCount = 0;
            while (toProcess.Any())
            {
                var current = toProcess.Dequeue();
                totalCardCount++;
                for (int i = 1; i <= current.WinnerCount; i++)
                {
                    toProcess.Enqueue(cardsById[current.Id + i]);
                }
            }

            return totalCardCount;
        }
    }
}
