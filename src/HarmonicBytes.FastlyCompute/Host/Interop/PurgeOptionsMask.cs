namespace HarmonicBytes.FastlyCompute.Host.Interop;

[Flags]
internal enum PurgeOptionsMask
{
    SoftPurge = 1 << 0,
    ReturnBuffer = 1 << 1,
}
