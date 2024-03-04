namespace Day20Problem1
{
    public class ConjunctionModule : Module
    {
        private readonly Dictionary<Module, PulseType> _lastPulseFromSource = [];

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            _lastPulseFromSource[pulse.Source!] = pulse.Type;
            PulseType typeToSend = _lastPulseFromSource.Values.All(p => p == PulseType.High)
                ? PulseType.Low
                : PulseType.High;
            return Destinations
                .Select(dest => new Pulse
                {
                    Source = this,
                    Destination = dest,
                    Type = typeToSend
                })
                .ToList();
        }

        public void ConfigureSource(Module source)
        {
            _lastPulseFromSource.Add(source, PulseType.Low);
        }
    }
}
