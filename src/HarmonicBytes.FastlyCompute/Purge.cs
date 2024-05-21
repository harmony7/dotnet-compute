namespace HarmonicBytes.FastlyCompute;

using HostPurge = Host.Purge;

public static class Purge
{
    public static string PurgeSurrogateKey(string surrogateKey)
    {
        return HostPurge.PurgeSurrogateKey(surrogateKey, false);
    }

    public static string SoftPurgeSurrogateKey(string surrogateKey)
    {
        return HostPurge.PurgeSurrogateKey(surrogateKey, true);
    }
}
