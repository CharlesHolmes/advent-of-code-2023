namespace Day19Problem2
{
    public class Solver
    {
        private readonly Dictionary<string, Rule> _rules = new Dictionary<string, Rule>();
        private readonly List<Dictionary<string, long>> _acceptedParts = new List<Dictionary<string, long>>();

        public long GetSolution(string[] inputLines)
        {
            int separatorLineIndex = inputLines.Select((s, i) => new { s = s, i = i }).Where(x => string.IsNullOrWhiteSpace(x.s)).Select(x => x.i).Single();
            var ruleLines = inputLines.Take(separatorLineIndex);
            foreach (string ruleLine in ruleLines)
            {
                ParseRuleLine(ruleLine);
            }

            ExploreRuleBoundaries("in", 0, GetDefaultRanges());
            long combination = GetCombination();
            return combination;
        }

        private RuleStepResult GetRuleStepResultFromChar(char c)
        {
            switch (c)
            {
                case 'A':
                    return RuleStepResult.Accept;
                case 'R':
                    return RuleStepResult.Reject;
                default:
                    throw new ArgumentException("Unrecognized rule step character", nameof(c));
            }
        }

        private void ParseRuleLine(string ruleLine)
        {
            string[] halves = ruleLine.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            string name = halves[0];
            string[] stepStrings = halves[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var ruleSteps = new List<RuleStep>();
            foreach (string stepString in stepStrings)
            {
                bool isDefaultStep = false;
                string? nextRule = null;
                Func<long, bool>? operation = null;
                RuleStepResult result;
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
                        throw new ArgumentException("Unrecognized rule expression operator");
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

            _rules.Add(name, new Rule(name, ruleSteps));
        }

        private void ProcessMatchedStep(RuleStep step, Dictionary<string, long> range)
        {
            if (step.Result == RuleStepResult.NextRule)
            {
                ExploreRuleBoundaries(step.NextRule!, 0, range);
            }
            else if (step.Result == RuleStepResult.Accept)
            {
                _acceptedParts.Add(range.ToDictionary());
            }
            else
            {
                // reject
            }
        }

        private string GetPropertySuffixForComparison(RuleStep step)
        {
            return step.Operator == '<' ? "_min" : "_max";
        }

        private string GetPropertySuffixForTightening(RuleStep step)
        {
            return step.Operator == '<' ? "_max" : "_min";
        }

        private long GetComparisonValue(RuleStep step, Dictionary<string, long> range)
        {
            string suffix = GetPropertySuffixForComparison(step);
            return range[step.Property + suffix];
        }

        private bool RangeMatchesStep(RuleStep step, Dictionary<string, long> range)
        {
            return step.Operation!(GetComparisonValue(step, range));
        }

        private Dictionary<string, long> TightenRange(RuleStep step, Dictionary<string, long> range)
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

        private bool CanMakeDisjoint(Dictionary<string, long> range)
        {
            foreach (char c in "xmas")
            {
                if (range[c + "_min"] > range[c + "_max"]) return false;
            }

            return true;
        }

        private Dictionary<string, long> MakeDisjoint(RuleStep step, Dictionary<string, long> range)
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

        private void ExploreRuleBoundaries(string ruleName, int stepIndex, Dictionary<string, long> ranges)
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
                if (CanMakeDisjoint(ranges))
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

        private Dictionary<string, long> GetDefaultRanges()
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

        private long GetCombination()
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
    }
}
