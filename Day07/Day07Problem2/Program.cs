namespace Day07Problem2
{
    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    public class Hand : IComparable<Hand>
    {
        private readonly Dictionary<char, int> _cardValues = new Dictionary<char, int>
        {
            { 'J', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
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
                if (cardCount.ContainsKey('J'))
                {
                    return HandType.FiveOfAKind;
                }
                else
                {
                    return HandType.FourOfAKind;
                }
            }
            else if (countCounts.ContainsKey(3))
            {
                if (countCounts.ContainsKey(2))
                {
                    if (cardCount.ContainsKey('J'))
                    {
                        return HandType.FiveOfAKind;
                    }
                    else
                    {
                        return HandType.FullHouse;
                    }
                }
                else
                {
                    if (cardCount.ContainsKey('J'))
                    {
                        return HandType.FourOfAKind;
                    }
                    else
                    {
                        return HandType.ThreeOfAKind;
                    }
                }
            }
            else if (countCounts.ContainsKey(2))
            {
                if (countCounts[2] == 2)
                {
                    if (cardCount.ContainsKey('J'))
                    {
                        if (cardCount['J'] == 2)
                        {
                            return HandType.FourOfAKind;
                        }
                        else
                        {
                            return HandType.FullHouse;
                        }
                    }
                    else
                    {
                        return HandType.TwoPair;
                    }
                }
                else
                {
                    if (cardCount.ContainsKey('J'))
                    {
                        return HandType.ThreeOfAKind;
                    }
                    else
                    {
                        return HandType.OnePair;
                    }
                }
            }
            else
            {
                if (cardCount.ContainsKey('J'))
                {
                    return HandType.OnePair;
                }
                else
                {
                    return HandType.HighCard;
                }
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

    public class Play
    {
        public Hand Hand { get; set; }
        public long Bid { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var playList = new List<Play>();
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            foreach (string playString in inputLines)
            {
                string[] split = playString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                playList.Add(new Play
                {
                    Hand = new Hand
                    {
                        Cards = split[0]
                    },
                    Bid = long.Parse(split[1])
                });
            }

            playList = playList.OrderBy(p => p.Hand).ToList();
            long totalWinnings = 0;
            long rank = 1;
            foreach (Play play in playList)
            {
                Console.Out.WriteLine(play.Hand.Cards);
                long playWinnings = play.Bid * rank;
                totalWinnings += playWinnings;
                rank++;
            }

            Console.Out.WriteLine(totalWinnings);
        }
    }
}
