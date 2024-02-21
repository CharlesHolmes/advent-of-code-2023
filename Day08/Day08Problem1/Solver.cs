namespace Day08Problem1
{
    public class Solver
    {
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
            Node? currentNode = nodeDict["AAA"];
            int stepIndex = 0;
            int stepCount = 0;
            while (currentNode != null && currentNode.Name != "ZZZ")
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

            return stepCount;
        }
    }
}
