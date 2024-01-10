namespace Day09Problem2
{
    public class Sequence
    {
        public List<long> Elements { get; init; } = new List<long>();
        public Sequence Derivative { get; set; }
        public long NextPrediction { get; set; }
    }

    internal class Program
    {
        static long PredictNext(Sequence sequence)
        {
            if (sequence.Derivative.Elements.All(x => x == 0))
            {
                return sequence.Elements.First();
            }
            else
            {
                return sequence.Elements.First() - PredictNext(sequence.Derivative);
            }
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            var sequences = new List<Sequence>();
            foreach (string line in inputLines)
            {
                sequences.Add(new Sequence
                {
                    Elements = line.Split(' ').Select(x => long.Parse(x)).ToList()
                });
            }

            foreach (Sequence inputSequence in sequences)
            {
                var currentSequence = inputSequence;
                List<long> derivativeElements;
                do
                {
                    derivativeElements = new List<long>();
                    for (int i = 1; i < currentSequence.Elements.Count; i++)
                    {
                        derivativeElements.Add(currentSequence.Elements[i] - currentSequence.Elements[i - 1]);
                    }

                    if (!derivativeElements.Any())
                    {
                        derivativeElements.Add(0);
                    }

                    currentSequence.Derivative = new Sequence
                    {
                        Elements = derivativeElements
                    };

                    currentSequence = currentSequence.Derivative;
                } while (!derivativeElements.All(x => x == 0));

                inputSequence.NextPrediction = PredictNext(inputSequence);
            }

            foreach (Sequence sequence in sequences)
            {
                Console.Out.WriteLine($"Sequence: {string.Join(',', sequence.Elements)}");
                Sequence derivative = sequence.Derivative;
                while (derivative != null)
                {
                    Console.Out.WriteLine($"  Derivative: {string.Join(',', derivative.Elements)}");
                    derivative = derivative.Derivative;
                }
                Console.Out.WriteLine($"  Prediction: {sequence.NextPrediction}");
            }
            Console.Out.WriteLine(sequences.Sum(x => x.NextPrediction));
        }
    }
}
