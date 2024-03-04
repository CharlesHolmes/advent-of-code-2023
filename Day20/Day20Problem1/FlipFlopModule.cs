namespace Day20Problem1
{
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
}
