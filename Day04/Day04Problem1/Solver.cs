namespace Day04Problem1
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var cards = new List<Card>();
            foreach (string cardLine in inputLines)
            {
                cards.Add(Card.Parse(cardLine));
            }

            return cards.Sum(c => c.GetCardScore());
        }
    }
}
