namespace Day20Problem2
{
    public class FlipFlopModule : Module
    {
        private bool _flipFlopSetOn = false;

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            if (pulse.Type == PulseType.High) return [];
            else
            {
                PulseType typeToSend = _flipFlopSetOn
                    ? PulseType.Low
                    : PulseType.High;
                _flipFlopSetOn = !_flipFlopSetOn;
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
        }
    }
}
