using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

// This is the Config Store.
// For historical reasons, this module is internally called fastly_dictionary
public static class FastlyDictionary
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int dictionary_open(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] name,
        int nameLength,
        out Handle dictionaryHandle
    );
    
    [DllImport(LibraryName)]
    public static extern int dictionary_get(
        Handle dictionaryHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] key,
        int keyLength,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] value,
        int valueMaxLength,
        out int bytesWritten
    );
}
