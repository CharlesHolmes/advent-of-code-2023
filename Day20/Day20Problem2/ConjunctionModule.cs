namespace Day20Problem2
{
    public class ConjunctionModule : Module
    {
        private readonly Dictionary<Module, PulseType> _lastPulseFromSource = new Dictionary<Module, PulseType>();
        public bool MarkedForCycleCheck { get; set; }
        public Action<long> RecordCycleLength { get; set; }
        private readonly Dictionary<Module, bool> _foundCycle = new Dictionary<Module, bool>();

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            if (MarkedForCycleCheck)
            {
                if (_lastPulseFromSource[pulse.Source] == PulseType.Low
                    && pulse.Type == PulseType.High
                    && !_foundCycle[pulse.Source])
                {
                    Console.Out.WriteLine($"{pulse.Source.Name} went high at {pulse.ButtonPresses} presses");
                    _foundCycle[pulse.Source] = true;
                    RecordCycleLength(pulse.ButtonPresses);
                }
            }

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
                    Type = typeToSend,
                    ButtonPresses = pulse.ButtonPresses
                });
            }

            return result;
        }

        public void ConfigureSource(Module source)
        {
            _lastPulseFromSource.Add(source, PulseType.Low);
            _foundCycle.Add(source, false);
        }
    }
}
