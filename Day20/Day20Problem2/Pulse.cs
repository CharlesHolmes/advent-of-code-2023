namespace Day20Problem2
{
    public class Pulse
    {
        public PulseType Type { get; init; }
        public Module Source { get; init; }
        public Module Destination { get; init; }
        public long ButtonPresses { get; init; }
    }
}
