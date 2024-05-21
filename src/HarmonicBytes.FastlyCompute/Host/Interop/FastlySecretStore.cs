using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

internal static class FastlySecretStore
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int secret_store_open(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] name,
        int nameLength,
        out Handle secretStoreHandle
    );

    [DllImport(LibraryName)]
    public static extern int secret_store_get(
        Handle secretStoreHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] key,
        int keyLength,
        out Handle secretStoreSecretHandle
    );

    [DllImport(LibraryName)]
    public static extern int secret_store_plaintext(
        Handle secretStoreSecretHandle,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] value,
        int valueMaxLength,
        out int bytesWritten
    );
}
