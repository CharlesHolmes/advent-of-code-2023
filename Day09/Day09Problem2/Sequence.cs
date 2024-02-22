namespace Day09Problem2
{
    public class Sequence
    {
        private List<long> Elements { get; init; }
        private Sequence? Derivative { get; init; }
        public long NextPrediction { get; private init; }

        public Sequence(IEnumerable<long> elements)
        {
            Elements = elements.ToList();
            Derivative = GetDerivative();
            NextPrediction = PredictNext();
        }

        private Sequence? GetDerivative()
        {
            if (Elements.TrueForAll(x => x == 0))
            {
                return null;
            }
            else 
            {
                var derivativeElements = new List<long>();
                for (int i = 1; i < Elements.Count; i++)
                {
                    derivativeElements.Add(Elements[i] - Elements[i - 1]);
                }

                if (!derivativeElements.Any())
                {
                    derivativeElements.Add(0);
                }

                return new Sequence(derivativeElements);
            }
        }

        private long PredictNext()
        {
            if (Derivative == null)
            {
                return 0;
            }
            else if (Derivative.Elements.TrueForAll(x => x == 0))
            {
                return Elements.First();
            }
            else
            {
                return Elements.First() - Derivative.NextPrediction;
            }
        }
    }
}
