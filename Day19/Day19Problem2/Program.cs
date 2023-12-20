namespace Day19Problem2
{
    public enum RuleStepResult
    {
        Unknown,
        Accept,
        Reject,
        NextRule
    }

    public class RuleStep
    {
        public bool IsDefaultStep { get; init; }
        public char Property { get; init; }
        public Func<long, bool> Operation { get; init; }
        public long BoundaryValue { get; init; }
        public char Operator { get; init; }
        public RuleStepResult Result { get; init; }
        public string NextRule { get; init; }
    }

    public class Rule
    {
        public string Name { get; init; }
        public List<RuleStep> Steps { get; init; }
    }

    internal class Program
    {
        private static readonly Dictionary<string, Rule> _rules = new Dictionary<string, Rule>();
        private static readonly List<Dictionary<string, long>> _acceptedParts = new List<Dictionary<string, long>>();

        private static RuleStepResult GetRuleStepResultFromChar(char c)
        {
            switch (c)
            {
                case 'A':
                    return RuleStepResult.Accept;
                case 'R':
                    return RuleStepResult.Reject;
                default:
                    throw new Exception("unrecognized default step single char");
            }
        }

        private static void ParseRuleLine(string ruleLine)
        {
            string[] halves = ruleLine.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            string name = halves[0];
            string[] stepStrings = halves[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var ruleSteps = new List<RuleStep>();
            foreach (string stepString in stepStrings)
            {
                bool isDefaultStep = false;
                string nextRule = null;
                Func<long, bool> operation = null;
                RuleStepResult result = RuleStepResult.Unknown;
                char oper = '\0';
                long boundaryValue = 0;
                char expressionPartSubject = '\0';
                if (stepString.IndexOf(':') == -1)
                {
                    isDefaultStep = true;
                    // this is a default
                    if (stepString.Length == 1 && char.IsUpper(stepString[0]))
                    {
                        result = GetRuleStepResultFromChar(stepString[0]);
                    }
                    else
                    {
                        result = RuleStepResult.NextRule;
                        nextRule = stepString;
                    }
                }
                else
                {
                    string[] splitOnColon = stepString.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    string ruleExpression = splitOnColon[0];
                    string[] expressionParts = ruleExpression.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                    expressionPartSubject = expressionParts[0][0];
                    boundaryValue = long.Parse(expressionParts[1]);
                    if (ruleExpression.Contains('<'))
                    {
                        oper = '<';
                        operation = x => x < boundaryValue;
                    }
                    else if (ruleExpression.Contains('>'))
                    {
                        oper = '>';
                        operation = x => x > boundaryValue;
                    }
                    else
                    {
                        throw new Exception("Unrecognized rule expression operator");
                    }

                    string target = splitOnColon[1];
                    if (target.Length == 1)
                    {
                        result = GetRuleStepResultFromChar(target[0]);
                    }
                    else
                    {
                        result = RuleStepResult.NextRule;
                        nextRule = target;
                    }
                }

                ruleSteps.Add(new RuleStep
                {
                    IsDefaultStep = isDefaultStep,
                    NextRule = nextRule,
                    Operation = operation,
                    Property = expressionPartSubject,
                    Result = result,
                    BoundaryValue = boundaryValue,
                    Operator = oper
                });
            }

            _rules.Add(name, new Rule
            {
                Name = name,
                Steps = ruleSteps
            });
        }

        private static void ProcessMatchedStep(RuleStep step, Dictionary<string, long> range)
        {
            if (step.Result == RuleStepResult.NextRule)
            {
                ExploreRuleBoundaries(step.NextRule, 0, range);
            }
            else if (step.Result == RuleStepResult.Accept)
            {
                _acceptedParts.Add(range.ToDictionary());
            }
            else
            {
                // reject
                return;
            }
        }

        private static string GetPropertySuffixForComparison(RuleStep step)
        {
            return step.Operator == '<' ? "_min" : "_max";
        }

        private static string GetPropertySuffixForTightening(RuleStep step)
        {
            return step.Operator == '<' ? "_max" : "_min";
        }

        private static long GetComparisonValue(RuleStep step, Dictionary<string, long> range)
        {
            string suffix = GetPropertySuffixForComparison(step);
            return range[step.Property + suffix];
        }

        private static bool RangeMatchesStep(RuleStep step, Dictionary<string, long> range)
        {
            return step.Operation(GetComparisonValue(step, range));
        }

        private static Dictionary<string, long> TightenRange(RuleStep step, Dictionary<string, long> range)
        {
            var result = range.ToDictionary();
            string suffix = GetPropertySuffixForTightening(step);
            if (step.Operator == '<')
            {
                result[step.Property + suffix] = step.BoundaryValue - 1;
            }
            else
            {
                result[step.Property + suffix] = step.BoundaryValue + 1;
            }

            return result;
        }

        private static bool CanMakeDisjoint(RuleStep step, Dictionary<string, long> range)
        {
            var newDict = MakeDisjoint(step, range);
            foreach (char c in "xmas")
            {
                if (range[c + "_min"] > range[c + "_max"]) return false;
            }

            return true;
        }

        private static Dictionary<string, long> MakeDisjoint(RuleStep step, Dictionary<string, long> range)
        {
            var result = range.ToDictionary();
            string suffix = GetPropertySuffixForComparison(step);
            if (step.Operator == '<')
            {
                result[step.Property + suffix] = step.BoundaryValue;
            }
            else
            {
                result[step.Property + suffix] = step.BoundaryValue;
            }

            return result;
        }

        private static void ExploreRuleBoundaries(string ruleName, int stepIndex, Dictionary<string, long> ranges)
        {
            var rule = _rules[ruleName];
            var step = rule.Steps[stepIndex];
            if (step.IsDefaultStep)
            {
                ProcessMatchedStep(step, ranges);
            }
            else if (RangeMatchesStep(step, ranges))
            {
                // tighten up to put the whole value in range, then process match
                ProcessMatchedStep(step, TightenRange(step, ranges));
                // constrain the range to make it not match (if possible), then move on to next step
                if (CanMakeDisjoint(step, ranges))
                {
                    ExploreRuleBoundaries(ruleName, stepIndex + 1, MakeDisjoint(step, ranges));
                }
            }
            else
            {
                // move on to next step without modifying range
                ExploreRuleBoundaries(ruleName, stepIndex + 1, ranges);
            }

            // for each step
            // if a match is already possible, explore it
            // if a match is possible by narrowing, then narrow and explore it
            // if a not-match is already the case, move to next step
            // if a not-match is possible by narrowing, then narrow and move to the next step
            // if next rule with operator match, make reduced range and recurse
            // if accept with operator match, make reduced range and commit reduced range to list
            // if reject, reduce range to make operator false
        }

        private static Dictionary<string, long> GetDefaultRanges()
        {
            return new Dictionary<string, long>
            {
                { "x_min", 1 },
                { "m_min", 1 },
                { "a_min", 1 },
                { "s_min", 1 },
                { "x_max", 4000 },
                { "m_max", 4000 },
                { "a_max", 4000 },
                { "s_max", 4000 }
            };
        }

        private static long GetCombination()
        {
            checked
            {
                long totalSum = 0;
                foreach (var part in _acceptedParts)
                {
                    long partProduct = 1;
                    foreach (char c in "xmas")
                    {
                        long rangeSize = part[c + "_max"] - part[c + "_min"] + 1;
                        partProduct *= rangeSize;
                    }

                    totalSum += partProduct;
                }

                return totalSum;
            }
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            int separatorLineIndex = inputLines.Select((s, i) => new { s = s, i = i }).Where(x => string.IsNullOrWhiteSpace(x.s)).Select(x => x.i).Single();
            var ruleLines = inputLines.Take(separatorLineIndex);
            foreach (string ruleLine in ruleLines)
            {
                ParseRuleLine(ruleLine);
            }

            ExploreRuleBoundaries("in", 0, GetDefaultRanges());
            Console.Out.Write(GetCombination());
        }
    }
}
