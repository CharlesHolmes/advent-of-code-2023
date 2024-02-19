namespace Day07Problem1
{
    public class Hand : IComparable<Hand>
    {
        private readonly Dictionary<char, int> _cardValues = new Dictionary<char, int>
        {
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'J', 11 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 }
        };

        public string Cards { get; set; }
        public HandType GetHandType()
        {
            var cardCount = new Dictionary<char, int>();
            foreach (char c in Cards)
            {
                if (!cardCount.ContainsKey(c))
                {
                    cardCount.Add(c, 1);
                }
                else
                {
                    cardCount[c]++;
                }
            }

            var countCounts = new Dictionary<int, int>();
            foreach (int count in cardCount.Values)
            {
                if (!countCounts.ContainsKey(count))
                {
                    countCounts.Add(count, 1);
                }
                else
                {
                    countCounts[count]++;
                }
            }

            if (countCounts.ContainsKey(5))
            {
                return HandType.FiveOfAKind;
            }
            else if (countCounts.ContainsKey(4))
            {
                return HandType.FourOfAKind;
            }
            else if (countCounts.ContainsKey(3))
            {
                if (countCounts.ContainsKey(2))
                {
                    return HandType.FullHouse;
                }
                else
                {
                    return HandType.ThreeOfAKind;
                }
            }
            else if (countCounts.ContainsKey(2))
            {
                if (countCounts[2] == 2)
                {
                    return HandType.TwoPair;
                }
                else
                {
                    return HandType.OnePair;
                }
            }
            else
            {
                return HandType.HighCard;
            }
        }

        public int CompareTo(Hand other)
        {
            int handTypeDiff = GetHandType() - other.GetHandType();
            if (handTypeDiff != 0)
            {
                return handTypeDiff;
            }
            else
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    int cardDiff = _cardValues[Cards[i]] - _cardValues[other.Cards[i]];
                    if (cardDiff != 0) return cardDiff;
                }
            }

            return 0;
        }
    }
}
