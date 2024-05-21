using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

// This is the KV Store.
// For historical reasons, this module is internally called fastly_object_store
internal static class FastlyObjectStore
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int object_store_open(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] name,
        int nameLength,
        out Handle objectStoreHandle
    );

    [DllImport(LibraryName)]
    public static extern int object_store_lookup(
        Handle objectStoreHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] key,
        int keyLength,
        out Handle bodyHandle
    );

    [DllImport(LibraryName)]
    public static extern int object_store_insert(
        Handle objectStoreHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] key,
        int keyLength,
        Handle bodyHandle
    );
}
