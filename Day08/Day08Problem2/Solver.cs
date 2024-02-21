namespace Day08Problem2
{
    public class Solver
    {
        private static long GetGreatestCommonFactor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long GetLeastCommonMultiple(long a, long b)
        {
            return (a / GetGreatestCommonFactor(a, b)) * b;
        }

        public long GetSolution(string[] inputLines)
        {
            var nodeDict = new Dictionary<string, Node>();
            foreach (string line in inputLines.Skip(2))
            {
                string nodeName = line.Substring(0, 3);
                var node = new Node
                {
                    Name = nodeName
                };
                nodeDict.Add(nodeName, node);
            }

            foreach (string line in inputLines.Skip(2))
            {
                string nodeName = line.Substring(0, 3);
                string leftNodeName = line.Substring(7, 3);
                string rightNodeName = line.Substring(12, 3);
                nodeDict[nodeName].Left = nodeDict[leftNodeName];
                nodeDict[nodeName].Right = nodeDict[rightNodeName];
            }

            string steps = inputLines[0];
            List<long> stepCounts = new List<long>();
            foreach (Node node in nodeDict.Values.Where(n => n.Name.EndsWith('A')))
            {
                Node? currentNode = node;
                long stepCount = 0;
                int stepIndex = 0;
                while (currentNode != null && !currentNode.Name.EndsWith('Z'))
                {
                    if (steps[stepIndex] == 'L')
                    {
                        currentNode = currentNode.Left;
                    }
                    else if (steps[stepIndex] == 'R')
                    {
                        currentNode = currentNode.Right;
                    }
                    else
                    {
                        throw new InvalidOperationException("Encountered an unrecognized step");
                    }

                    stepCount++;
                    stepIndex++;
                    if (stepIndex >= steps.Length)
                    {
                        stepIndex = 0;
                    }
                }

                stepCounts.Add(stepCount);
            }

            return stepCounts.Aggregate(1L, GetLeastCommonMultiple);
        }
    }
}
