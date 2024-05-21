namespace HarmonicBytes.FastlyCompute.Host;

internal static class Constants
{
    public const int MaxMethodLength = 1024;
    public const int MaxUriLength = 8192;
    public const int MaxHeaderLength = 69000;
    public const int MaxDictionaryEntryLength = 8000;

    public const int HostcallBufferLength = MaxHeaderLength;
}
