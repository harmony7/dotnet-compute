using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

internal static class FastlyPurge
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int purge_surrogate_key(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] surrogateKey,
        int surrogateKeyLength,
        uint optionsMask,
        ref FastlyPurgeOptions fastlyPurgeOptions 
    );
}
