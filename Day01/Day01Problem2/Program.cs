namespace Day01Problem2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] numberWords = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] inputLines = (await File.ReadAllTextAsync(args[0])).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            long sum = 0;
            foreach (string line in inputLines)
            {
                //Console.Out.WriteLine(line);
                var firstDigit = line.Select((c, i) => new { Char = c, Index = i }).First(x => char.IsDigit(x.Char));
                int firstWordIndex = int.MaxValue;
                int firstWordValue = 0;
                for (int i = 0; i < numberWords.Length; i++)
                {
                    string word = numberWords[i];
                    int wordIndex = line.IndexOf(word);
                    if (wordIndex >= 0 && wordIndex < firstWordIndex)
                    {
                        firstWordIndex = wordIndex;
                        firstWordValue = i + 1;
                    }
                }

                if (firstDigit.Index < firstWordIndex)
                {
                    sum += (firstDigit.Char - '0') * 10;
                    //Console.Out.WriteLine((firstDigit.Char - '0'));
                }
                else
                {
                    sum += firstWordValue * 10;
                    //Console.Out.WriteLine(firstWordValue);
                }

                var lastDigit = line.Select((c, i) => new { Char = c, Index = i }).Last(x => char.IsDigit(x.Char));
                int lastWordIndex = -1;
                int lastWordValue = 0;
                for (int i = 0; i < numberWords.Length; i++)
                {
                    string word = numberWords[i];
                    int wordIndex = line.LastIndexOf(word);
                    if (wordIndex >= 0 && wordIndex > lastWordIndex)
                    {
                        lastWordIndex = wordIndex;
                        lastWordValue = i + 1;
                    }
                }

                if (lastDigit.Index > lastWordIndex)
                {
                    sum += lastDigit.Char - '0';
                }
                else
                {
                    sum += lastWordValue;
                }
            }

            Console.Out.WriteLine($"Your overall sum is: {sum}");
        }
    }
}
