namespace Day20Problem1
{
    public class FlipFlopModule : Module
    {
        private bool _flipFlopOn = false;

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            if (pulse.Type == PulseType.High) return [];
            else
            {
                PulseType typeToSend = _flipFlopOn
                    ? PulseType.Low
                    : PulseType.High;
                _flipFlopOn = !_flipFlopOn;
                return Destinations
                    .Select(dest => new Pulse
                    {
                        Source = this,
                        Destination = dest,
                        Type = typeToSend
                    })
                    .ToList();
            }
        }
    }
}
