using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

internal static class FastlyHttpBody
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int body_new(
        out Handle bodyHandle
    );

    [DllImport(LibraryName)]
    public static extern int body_read(
        Handle bodyHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
        int bufferSize,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int body_write(
        Handle bodyHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
        int bufferSize,
        int bodyWriteEnd,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int body_close(
        Handle bodyHandle
    );
}
