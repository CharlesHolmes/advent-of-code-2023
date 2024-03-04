namespace Day20Problem1
{
    public class Solver
    {
        private readonly Dictionary<string, Module> _modules = new Dictionary<string, Module>();
        private long _lowPulseCount = 0;
        private long _highPulseCount = 0;

        public long GetSolution(string[] inputLines)
        {
            CreateModules(inputLines);
            WireUpModuleConnections(inputLines);
            for (int i = 0; i < 1000; i++)
            {
                HandleButtonPress();
            }

            return _lowPulseCount * _highPulseCount;
        }

        private void CreateModules(string[] inputLines)
        {
            foreach (string line in inputLines)
            {
                string moduleText = line.Split(new[] { ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (moduleText.StartsWith('%'))
                {
                    string moduleName = moduleText.Substring(1);
                    _modules.Add(moduleName, new FlipFlopModule { Name = moduleName });
                }
                else if (moduleText.StartsWith('&'))
                {
                    string moduleName = moduleText.Substring(1);
                    _modules.Add(moduleName, new ConjunctionModule { Name = moduleName });
                }
                else if (moduleText.Equals("broadcaster"))
                {
                    _modules.Add(moduleText, new BroadcasterModule { Name = moduleText });
                }
                else
                {
                    throw new ArgumentException($"Unrecognized module text prefix: {moduleText}", nameof(inputLines));
                }
            }
        }

        private void WireUpModuleConnections(string[] inputLines)
        {
            foreach (string line in inputLines)
            {
                string[] halves = line.Split('>', StringSplitOptions.RemoveEmptyEntries);
                string moduleName = halves[0].Split(new[] { '%', '&', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string[] destinations = halves[1].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string destination in destinations)
                {
                    if (_modules.TryGetValue(destination, out Module? destinationModule))
                    {
                        _modules[moduleName].Destinations.Add(destinationModule);
                        if (destinationModule is ConjunctionModule conjunctionModule)
                        {
                            conjunctionModule.ConfigureSource(_modules[moduleName]);
                        }
                    }
                    else
                    {
                        _modules[moduleName].Destinations.Add(new SinkModule { Name = destination });
                    }
                }
            }
        }

        private void HandleButtonPress()
        {
            var pulsesToProcess = new Queue<Pulse>();
            pulsesToProcess.Enqueue(new Pulse
            {
                Source = null,
                Destination = _modules["broadcaster"],
                Type = PulseType.Low
            });

            while (pulsesToProcess.Any())
            {
                var current = pulsesToProcess.Dequeue();
                if (current.Type == PulseType.Low)
                {
                    _lowPulseCount++;
                }
                else
                {
                    _highPulseCount++;
                }

                List<Pulse> output = current.Destination!.HandlePulse(current);
                foreach (Pulse pulse in output)
                {
                    pulsesToProcess.Enqueue(pulse);
                }
            }
        }
    }
}
