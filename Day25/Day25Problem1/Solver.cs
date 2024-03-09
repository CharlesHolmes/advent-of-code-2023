namespace Day25Problem1
{
    public class Solver
    {
        private static readonly char[] separator = new char[] { ':', ' ' };

        public long GetSolution(string[] inputLines)
        {
            var nodesByName = new Dictionary<string, Node>();
            var allEdges = new List<Edge>();
            foreach (string line in inputLines)
            {
                string[] componentNames = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                string sourceNodeName = componentNames[0];
                if (!nodesByName.TryGetValue(sourceNodeName, out Node? sourceNode))
                {
                    sourceNode = new Node { Name = sourceNodeName };
                    nodesByName.Add(sourceNodeName, sourceNode);
                }

                foreach (string destinationNodeName in componentNames.Skip(1))
                {
                    if (!nodesByName.TryGetValue(destinationNodeName, out Node? destinationNode))
                    {
                        destinationNode = new Node { Name = destinationNodeName };
                        nodesByName.Add(destinationNodeName, destinationNode);
                    }

                    var sourceToDestination = new Edge { Source = sourceNode, Destination = destinationNode };
                    var destinationToSource = new Edge { Source = destinationNode, Destination = sourceNode };
                    sourceToDestination.TwinEdge = destinationToSource;
                    destinationToSource.TwinEdge = sourceToDestination;
                    sourceNode.Edges.Add(sourceToDestination);
                    destinationNode.Edges.Add(destinationToSource);
                    allEdges.Add(sourceToDestination);
                    allEdges.Add(destinationToSource);
                }
            }

            var topEdges = GetTopEdges(nodesByName.Values.ToList(), 500);
            long connectedComponent1Size = -1;
            long connectedComponent2Size = -1;
            var tokenSource = new CancellationTokenSource();
            for (int i = 0; i < topEdges.Count && !tokenSource.IsCancellationRequested; i++)
            {
                try
                {
                    Parallel.For(i + 1, topEdges.Count, new ParallelOptions { CancellationToken = tokenSource.Token }, j =>
                    {
                        for (int k = j + 1; k < topEdges.Count && !tokenSource.IsCancellationRequested; k++)
                        {
                            var deleted = new HashSet<Edge> { topEdges[i], topEdges[j], topEdges[k], topEdges[i].TwinEdge, topEdges[j].TwinEdge, topEdges[k].TwinEdge };
                            List<int> connectedComponentSizes = GetConnectedComponentSizes(nodesByName, deleted);
                            if (connectedComponentSizes.Count == 2)
                            {
                                connectedComponent1Size = connectedComponentSizes[0];
                                connectedComponent2Size = connectedComponentSizes[connectedComponentSizes.Count - 1];
                                tokenSource.Cancel();
                            }
                        }
                    });
                }
                catch (OperationCanceledException) { }

                Console.Out.WriteLine($"{(double)i / topEdges.Count * 100:0.00} percent complete.");
            }

            long connectedComponentSizeProduct = connectedComponent1Size * connectedComponent2Size;
            return connectedComponentSizeProduct;
        }

        private static List<Edge> GetTopEdges(List<Node> allNodes, int topN)
        {
            var edgeUsage = new Dictionary<Edge, int>();
            var random = new Random();
            for (int i = 0; i < 1000; i += 2)
            {
                Node n1 = allNodes[random.Next(allNodes.Count)];
                Node n2;
                do
                {
                    n2 = allNodes[random.Next(allNodes.Count)];
                } while (n1 == n2);

                var remaining = new Queue<Node>();
                var visited = new HashSet<Node>();
                remaining.Enqueue(n1);
                while (remaining.Any())
                {
                    var current = remaining.Dequeue();
                    visited.Add(current);
                    foreach (Edge edge in current.Edges)
                    {
                        if (!visited.Contains(edge.Destination))
                        {
                            if (!edgeUsage.ContainsKey(edge))
                            {
                                edgeUsage.Add(edge, 1);
                            }
                            else
                            {
                                edgeUsage[edge]++;
                            }

                            if (!edgeUsage.ContainsKey(edge.TwinEdge))
                            {
                                edgeUsage.Add(edge.TwinEdge, 1);
                            }
                            else
                            {
                                edgeUsage[edge.TwinEdge]++;
                            }

                            remaining.Enqueue(edge.Destination);
                        }
                    }
                }
            }

            return edgeUsage.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key).Take(topN).ToList();
        }


        private static List<Node> GetConnectedComponentsFrom(Node start, HashSet<Edge> deleted)
        {
            var visited = new HashSet<Node>();
            var toVisit = new Queue<Node>();
            toVisit.Enqueue(start);
            while (toVisit.Any())
            {
                var current = toVisit.Dequeue();
                visited.Add(current);
                foreach (Edge edge in current.Edges)
                {
                    if (!deleted.Contains(edge) 
                        && !visited.Contains(edge.Destination))
                    {
                        toVisit.Enqueue(edge.Destination);
                    }
                }
            }

            return visited.ToList();
        }

        private static List<int> GetConnectedComponentSizes(Dictionary<string, Node> nodes, HashSet<Edge> deleted)
        {
            var allVisited = new HashSet<Node>();
            var connectedComponents = new List<Node[]>();
            foreach (Node node in nodes.Values)
            {
                if (!allVisited.Contains(node))
                {
                    List<Node> connected = GetConnectedComponentsFrom(node, deleted);
                    foreach (Node c in connected) allVisited.Add(c);
                    connectedComponents.Add(connected.ToArray());
                }
            }

            return connectedComponents.Select(arr => arr.Length).ToList();
        }
    }
}
