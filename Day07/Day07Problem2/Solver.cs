namespace Day07Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            var playList = new List<Play>();
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

            return totalWinnings;
        }
    }
}
