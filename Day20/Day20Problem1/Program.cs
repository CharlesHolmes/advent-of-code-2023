namespace Day20Problem1
{
    public enum PulseType
    {
        Low,
        High
    }

    public class Pulse
    {
        public PulseType Type { get; init; }
        public Module Source { get; init; }
        public Module Destination { get; init; }
    }

    public abstract class Module
    {
        public string Name { get; init; }
        public List<Module> Destinations { get; } = new List<Module>();
        public abstract List<Pulse> HandlePulse(Pulse pulse);
    }

    public class FlipFlopModule : Module
    {
        private enum FlipFlopState
        {
            Off,
            On
        }

        private FlipFlopState state = FlipFlopState.Off;

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            if (pulse.Type == PulseType.High) return new List<Pulse>();
            else
            {
                PulseType typeToSend;
                if (state == FlipFlopState.Off)
                {
                    state = FlipFlopState.On;
                    typeToSend = PulseType.High;
                }
                else
                {
                    state = FlipFlopState.Off;
                    typeToSend = PulseType.Low;
                }

                var result = new List<Pulse>();
                foreach (Module module in Destinations)
                {
                    result.Add(new Pulse
                    {
                        Source = this,
                        Destination = module,
                        Type = typeToSend
                    });
                }

                return result;
            }
        }
    }

    public class ConjunctionModule : Module
    {
        private readonly Dictionary<Module, PulseType> _lastPulseFromSource = new Dictionary<Module, PulseType>();

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            _lastPulseFromSource[pulse.Source] = pulse.Type;
            PulseType typeToSend;
            if (_lastPulseFromSource.Values.All(p => p == PulseType.High))
            {
                typeToSend = PulseType.Low;
            }
            else
            {
                typeToSend = PulseType.High;
            }

            var result = new List<Pulse>();
            foreach (Module destination in Destinations)
            {
                result.Add(new Pulse
                {
                    Source = this,
                    Destination = destination,
                    Type = typeToSend
                });
            }

            return result;
        }

        public void ConfigureSource(Module source)
        {
            _lastPulseFromSource.Add(source, PulseType.Low);
        }
    }

    public class BroadcasterModule : Module
    {
        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            var result = new List<Pulse>();
            foreach (Module destination in Destinations)
            {
                result.Add(new Pulse
                {
                    Source = this,
                    Destination = destination,
                    Type = pulse.Type
                });
            }

            return result;
        }
    }

    public class SinkModule : Module
    {
        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            // do nothing?
            return new List<Pulse>();
        }
    }

    internal class Program
    {
        private static readonly Dictionary<string, Module> _modules = new Dictionary<string, Module>();
        private static long _lowPulseCount = 0;
        private static long _highPulseCount = 0;

        private static void CreateModules(string[] inputLines)
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
                    throw new Exception("What is this?");
                }
            }
        }

        private static void WireUpModuleConnections(string[] inputLines)
        {
            foreach (string line in inputLines)
            {
                string[] halves = line.Split('>', StringSplitOptions.RemoveEmptyEntries);
                string moduleName = halves[0].Split(new[] { '%', '&', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string[] destinations = halves[1].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string destination in destinations)
                {
                    if (_modules.TryGetValue(destination, out Module destinationModule))
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

        private static void HandleButtonPress()
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

                List<Pulse> output = current.Destination.HandlePulse(current);
                foreach (Pulse pulse in output)
                {
                    pulsesToProcess.Enqueue(pulse);
                }
            }
        }

        static async Task Main(string[] args)
        {
            var inputLines = await File.ReadAllLinesAsync(args[0]);
            CreateModules(inputLines);
            WireUpModuleConnections(inputLines);
            for (int i = 0; i < 1000; i++)
            {
                HandleButtonPress();
            }

            Console.Out.Write(_lowPulseCount * _highPulseCount);
        }
    }
}
