using System.Text;

namespace Day02Problem2
{
    public class Solver
    {
        public long GetSolution(string[] inputLines)
        {
            long gamePowerSum = 0;
            foreach (string line in inputLines)
            {
                string trimmed = line.Substring(5);
                string gameIdString = trimmed.Substring(0, trimmed.IndexOf(':'));
                int gameId = int.Parse(gameIdString);
                var gameMinimums = new Dictionary<string, int>();
                string[] draws = trimmed.Substring(trimmed.IndexOf(':') + 1).Split(';');
                foreach (string draw in draws)
                {
                    string[] drawCubeStrings = draw.Split(',');
                    foreach (string s in drawCubeStrings)
                    {
                        var numberBuilder = new StringBuilder();
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (s[i] == ' ') continue;
                            else if (char.IsLetter(s[i])) break;
                            else if (char.IsDigit(s[i])) numberBuilder.Append(s[i]);
                            else throw new Exception("what");
                        }

                        int count = int.Parse(numberBuilder.ToString());

                        var colorBuilder = new StringBuilder();
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (s[i] == ' ') continue;
                            else if (char.IsDigit(s[i])) continue;
                            else if (char.IsLetter(s[i])) colorBuilder.Append(s[i]);
                            else throw new Exception("what2");
                        }

                        string color = colorBuilder.ToString();

                        if (!gameMinimums.ContainsKey(color))
                        {
                            gameMinimums.Add(color, count);
                        }
                        else
                        {
                            gameMinimums[color] = Math.Max(gameMinimums[color], count);
                        }
                    }
                }

                gamePowerSum += gameMinimums.Values.Aggregate(1, (x, y) => x * y);
            }

            return gamePowerSum;
        }
    }
}
