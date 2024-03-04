namespace Day20Problem1
{
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
}
