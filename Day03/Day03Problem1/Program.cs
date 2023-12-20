namespace Day03Problem1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            long partNumberSum = 0;
            for (int i = 0; i < inputLines.Length; i++)
            {
                bool currentNumberBordersSymbol = false;
                int currentNumber = 0;
                // iterate over line
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    if (char.IsDigit(inputLines[i][j]))
                    {
                        currentNumber = currentNumber * 10;
                        currentNumber += inputLines[i][j] - '0';
                        if (BordersSymbol(inputLines, i, j)) currentNumberBordersSymbol = true;
                    }
                    else
                    {
                        if (currentNumber > 0)
                        {
                            if (currentNumberBordersSymbol)
                            {
                                Console.Out.WriteLine(currentNumber);
                                partNumberSum += currentNumber;
                            }

                            currentNumber = 0;
                            currentNumberBordersSymbol = false;
                        }
                    }
                }

                if (currentNumber > 0)
                {
                    if (currentNumberBordersSymbol)
                    {
                        Console.Out.WriteLine(currentNumber);
                        partNumberSum += currentNumber;
                    }
                }
            }

            Console.Out.WriteLine(partNumberSum);
        }

        private static bool BordersSymbol(string[] inputLines, int i, int j)
        {
            for (int idelta = -1; idelta <= 1; idelta++) 
            {
                for (int jdelta = -1; jdelta <= 1; jdelta++)
                {
                    if (i + idelta < 0) continue;
                    if (i + idelta >= inputLines.Length) continue;
                    if (j + jdelta < 0) continue;
                    if (j + jdelta >= inputLines[i + idelta].Length) continue;
                    char c = inputLines[i + idelta][j + jdelta];
                    if (!char.IsDigit(c) && c != '.') 
                        return true;
                }
            }

            return false;
        }
    }
}