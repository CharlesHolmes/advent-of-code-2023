namespace Day20Problem2
{
    public class ConjunctionModule : Module
    {
        private readonly Dictionary<Module, PulseType> _lastPulseFromSource = [];
        private readonly Dictionary<Module, bool> _foundCycle = [];
        public bool MarkedForCycleCheck { get; set; }
        public Action<long>? RecordCycleLength { get; set; }

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            if (pulse.Source == null) throw new ArgumentException("Pulse source cannot be null.", nameof(pulse));
            if (MarkedForCycleCheck 
                && _lastPulseFromSource[pulse.Source] == PulseType.Low
                && pulse.Type == PulseType.High
                && !_foundCycle[pulse.Source])
            {
                _foundCycle[pulse.Source] = true;
                if (RecordCycleLength == null) throw new InvalidOperationException("RecordCycleLength action cannot be null.");
                RecordCycleLength(pulse.ButtonPresses);
            }

            _lastPulseFromSource[pulse.Source] = pulse.Type;
            PulseType typeToSend = _lastPulseFromSource.Values.All(p => p == PulseType.High)
                ? PulseType.Low
                : PulseType.High;
            return Destinations
                .Select(dest => new Pulse
                {
                    Source = this,
                    Destination = dest,
                    Type = typeToSend,
                    ButtonPresses = pulse.ButtonPresses
                })
                .ToList();
        }

        public void ConfigureSource(Module source)
        {
            _lastPulseFromSource.Add(source, PulseType.Low);
            _foundCycle.Add(source, false);
        }
    }
}
