namespace Day19Problem1
{
    public class Rule
    {
        public string Name { get; init; }
        public List<RuleStep> Steps { get; init; }

        public Rule(string name, List<RuleStep> steps)
        {
            Name = name;
            Steps = steps;
        }
    }
}
