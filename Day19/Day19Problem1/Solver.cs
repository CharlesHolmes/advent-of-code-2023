namespace Day19Problem1
{
    public class Solver
    {
        private readonly Dictionary<string, Rule> _rules = new Dictionary<string, Rule>();
        private readonly List<Part> _parts = new List<Part>();

        public long GetSolution(string[] inputLines)
        {
            int separatorLineIndex = inputLines.Select((s, i) => new { s = s, i = i }).Where(x => string.IsNullOrWhiteSpace(x.s)).Select(x => x.i).Single();
            var ruleLines = inputLines.Take(separatorLineIndex);
            foreach (string ruleLine in ruleLines)
            {
                ParseRuleLine(ruleLine);
            }

            var partLines = inputLines.Skip(separatorLineIndex + 1);
            foreach (string partLine in partLines)
            {
                ParsePartLine(partLine);
            }

            DispositionParts();

            // iterate over part lines and determine final results
            // add up xmas numbers of accepted parts
            // need to track acceptance of each part for this

            long sumOfAcceptedPartRatingSums = _parts.Where(x => x.FinalResult == RuleStepResult.Accept).Sum(x => x.RatingSum);
            return sumOfAcceptedPartRatingSums;
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
                    throw new ArgumentException("Unrecognized rule step result char", nameof(c));
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
                    long expressionPartComparisonValue = long.Parse(expressionParts[1]);
                    if (ruleExpression.Contains('<'))
                    {
                        operation = x => x < expressionPartComparisonValue;
                    }
                    else if (ruleExpression.Contains('>'))
                    {
                        operation = x => x > expressionPartComparisonValue;
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
                    Result = result
                });
            }

            _rules.Add(name, new Rule(name, ruleSteps));
        }

        private void ParsePartLine(string partLine)
        {
            var ratings = new Dictionary<char, long>();
            foreach (string attributeValuePair in partLine.Split(new[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] halves = attributeValuePair.Split('=', StringSplitOptions.RemoveEmptyEntries);
                char attributeName = halves[0][0];
                int attributeValue = int.Parse(halves[1]);
                ratings.Add(attributeName, attributeValue);
            }

            _parts.Add(new Part(ratings));
        }

        private void DispositionParts()
        {
            foreach (Part part in _parts)
            {
                bool partDispositioned = false;
                Rule currentRule = _rules["in"];
                int stepIndex = 0;
                while (!partDispositioned)
                {
                    RuleStep step = currentRule.Steps[stepIndex];
                    bool applyStep = step.IsDefaultStep || step.Operation!(part.Ratings[step.Property]);
                    if (applyStep)
                    {
                        if (step.Result == RuleStepResult.NextRule)
                        {
                            currentRule = _rules[step.NextRule!];
                            stepIndex = 0;
                        }
                        else if (step.Result == RuleStepResult.Accept || step.Result == RuleStepResult.Reject)
                        {
                            part.FinalResult = step.Result;
                            partDispositioned = true;
                        }
                        else
                        {
                            throw new InvalidOperationException("Unrecognized step result");
                        }
                    }
                    else
                    {
                        stepIndex++;
                    }
                }
            }
        }
    }
}
