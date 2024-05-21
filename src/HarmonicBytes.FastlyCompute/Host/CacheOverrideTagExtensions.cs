namespace HarmonicBytes.FastlyCompute.Host;

internal static class CacheOverrideTagExtensions
{
    public static uint BuildTag(this CacheOverride cacheOverride) =>
        CacheOverrideTag.BuildCacheOverrideTagFor(cacheOverride);
}
