namespace Day20Problem2
{
    public class BroadcasterModule : Module
    {
        public override List<Pulse> HandlePulse(Pulse pulse) =>
            Destinations
                .Select(dest => new Pulse
                {
                    Source = this,
                    Destination = dest,
                    Type = pulse.Type,
                    ButtonPresses = pulse.ButtonPresses
                })
                .ToList();
    }
}
