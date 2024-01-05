using System.Text;

namespace Day02Problem1
{
    public class Solver
    {
        private readonly Dictionary<string, int> _possible = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        public long GetSolution(string[] inputLines)
        {
            int possibleGameIdSum = 0;
            foreach (string line in inputLines)
            {
                string trimmed = line.Substring(5);
                string gameIdString = trimmed.Substring(0, trimmed.IndexOf(':'));
                int gameId = int.Parse(gameIdString);
                string[] draws = trimmed.Substring(trimmed.IndexOf(':') + 1).Split(';');
                bool allDrawsLegal = true;
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

                        if (_possible[color] < count)
                        {
                            allDrawsLegal = false;
                            break;
                        }
                    }

                    if (!allDrawsLegal) break;
                }

                if (allDrawsLegal)
                {
                    possibleGameIdSum += gameId;
                }
            }

            return possibleGameIdSum;
        }
    }
}
