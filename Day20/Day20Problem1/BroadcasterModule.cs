namespace Day20Problem1
{
    public class BroadcasterModule : Module
    {
        public override List<Pulse> HandlePulse(Pulse pulse) =>
            Destinations
                .Select(dest => new Pulse
                {
                    Source = this,
                    Destination = dest,
                    Type = pulse.Type
                })
                .ToList();
    }
}
