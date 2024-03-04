namespace Day20Problem2
{
    public class Solver
    {
        private readonly Dictionary<string, Module> _modules = [];
        private readonly List<long> _cycleLengths = [];
        private int _cycleLengthExpectedCount = 0;

        public long GetSolution(string[] inputLines)
        {
            CreateModules(inputLines);
            WireUpModuleConnections(inputLines);
            long buttonPresses = 0;
            while (_cycleLengths.Count != _cycleLengthExpectedCount)
            {
                buttonPresses++;
                HandleButtonPress(buttonPresses);

                if (buttonPresses % 1000000 == 0)
                {
                    Console.Out.WriteLine($"Made {buttonPresses} button presses...");
                }
            }

            long runningLcm = 1;
            foreach (long cycle in _cycleLengths)
            {
                runningLcm = GetLeastCommonMultiple(runningLcm, cycle);
            }

            return runningLcm;
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
                            if (destination == "hp")
                            {
                                conjunctionModule.MarkedForCycleCheck = true;
                                conjunctionModule.RecordCycleLength = _cycleLengths.Add;
                                _cycleLengthExpectedCount++;
                            }
                        }
                    }
                    else
                    {
                        _modules[moduleName].Destinations.Add(new SinkModule { Name = destination });
                    }
                }
            }
        }

        private void HandleButtonPress(long presses)
        {
            var pulsesToProcess = new Queue<Pulse>();
            pulsesToProcess.Enqueue(new Pulse
            {
                Source = null,
                Destination = _modules["broadcaster"],
                Type = PulseType.Low,
                ButtonPresses = presses
            });

            while (pulsesToProcess.Any())
            {
                var current = pulsesToProcess.Dequeue();
                List<Pulse> output = current.Destination!.HandlePulse(current);
                foreach (Pulse pulse in output)
                {
                    pulsesToProcess.Enqueue(pulse);
                }
            }
        }

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
    }
}
