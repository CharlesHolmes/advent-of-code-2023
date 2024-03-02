namespace Day19Problem2
{
    public class RuleStep
    {
        public bool IsDefaultStep { get; init; }
        public char Property { get; init; }
        public Func<long, bool>? Operation { get; init; }
        public long BoundaryValue { get; init; }
        public char Operator { get; init; }
        public RuleStepResult Result { get; init; }
        public string? NextRule { get; init; }
    }
}
