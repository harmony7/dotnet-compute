namespace HarmonicBytes.FastlyCompute;

public class CacheOverride
{
    public CacheOverride(string mode, CacheOverrideInit? init = null)
    {
        if (mode != "none" && mode != "pass" && mode != "override")
        {
            throw new ArgumentException("CacheOverride: mode must be one of 'none', 'pass', 'override'.");
        }
        Mode = mode;

        if (Mode == "override")
        {
            if (init == null)
            {
                throw new ArgumentException("CacheOverride: 'init' must be provided when mode is 'override'.");
            }

            Pci = init.Pci;
            SurrogateKey = init.SurrogateKey;
            StaleWhileRevalidate = init.StateWhileRevalidate;
            TimeToLive = init.TimeToLive;
        }
    }

    public string Mode { get; }

    public bool? Pci { get; }
    public string? SurrogateKey { get; }
    public uint? StaleWhileRevalidate { get; }
    public uint? TimeToLive { get; }
}
