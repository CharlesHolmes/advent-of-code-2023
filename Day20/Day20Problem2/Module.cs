﻿namespace Day20Problem2
{
    public abstract class Module
    {
        public string? Name { get; init; }
        public List<Module> Destinations { get; } = [];
        public abstract List<Pulse> HandlePulse(Pulse pulse);
    }
}
