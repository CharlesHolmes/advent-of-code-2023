namespace Day01Problem1
{
    public class LineFactory : ILineFactory
    {
        public ILine Create(string input)
        {
            return new Line(input);
        }
    }
}
