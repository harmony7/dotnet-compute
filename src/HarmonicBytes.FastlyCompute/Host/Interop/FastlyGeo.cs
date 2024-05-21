using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

internal static class FastlyGeo
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int geo_lookup(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] addressOctets,
        int addressOctetsLength,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] buffer,
        int bufferLength,
        out int bytesWritten
    );
}
