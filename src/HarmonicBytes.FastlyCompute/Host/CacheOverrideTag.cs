namespace HarmonicBytes.FastlyCompute.Host;

internal static class CacheOverrideTag
{
    public const uint CacheOverrideNone = 0u;
    public const uint CacheOverridePass = 1u << 0;
    public const uint CacheOverrideTtl = 1u << 1;
    public const uint CacheOverrideStaleWhileRevalidate = 1u << 2;
    public const uint CacheOverridePci = 1u << 3;
    
    public static uint BuildCacheOverrideTagFor(CacheOverride cacheOverride)
    {
        uint value = CacheOverrideNone;

        if (cacheOverride.Mode == "none")
        {
            return value;
        }
        if (cacheOverride.Mode == "pass")
        {
            value |= CacheOverridePass;
            return value;
        }

        if (cacheOverride.TimeToLive != null)
        {
            value |= CacheOverrideTtl;
        }

        if (cacheOverride.StaleWhileRevalidate != null)
        {
            value |= CacheOverrideStaleWhileRevalidate;
        }

        if (cacheOverride.Pci != null)
        {
            value |= CacheOverridePci;
        }

        return value;
    } 
}