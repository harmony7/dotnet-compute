namespace HarmonicBytes.FastlyCompute.Http;

public class FastlyHttpOptions
{
    public static HttpRequestOptionsKey<FastlyHttpOptions> OptionsKey = new("FastlyHttpOptions");
    public string? Backend { get; set; }
    public CacheOverride? CacheOverride { get; set; }
    public string? CacheKey { get; set; }
}
