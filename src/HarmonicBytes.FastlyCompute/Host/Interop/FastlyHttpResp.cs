using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

internal static class FastlyHttpResp
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int resp_new(
        out Handle responseHandle
    );

    [DllImport(LibraryName)]
    public static extern int resp_send_downstream(
        Handle responseHandle,
        Handle bodyHandle,
        bool streaming
    );

    [DllImport(LibraryName)]
    public static extern int resp_status_get(
        Handle responseHandle,
        out ushort status
    );

    [DllImport(LibraryName)]
    public static extern int resp_status_set(
        Handle responseHandle,
        ushort status
    );

    [DllImport(LibraryName)]
    public static extern int resp_version_get(
        Handle responseHandle,
        out int version
    );

    [DllImport(LibraryName)]
    public static extern int resp_version_set(
        Handle responseHandle,
        int version
    );

    [DllImport(LibraryName)]
    public static extern int resp_header_names_get(
        Handle responseHandle,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
        int bufferLength,
        int cursor,
        out int endingCursor,
        out int length
    );

    [DllImport(LibraryName)]
    public static extern int resp_header_values_get(
        Handle responseHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] buffer,
        int bufferLength,
        int cursor,
        out int endingCursor,
        out int length
    );    

    [DllImport(LibraryName)]
    public static extern int resp_header_insert(
        Handle responseHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] value,
        int valueLength
    );

    [DllImport(LibraryName)]
    public static extern int resp_header_append(
        Handle responseHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] value,
        int valueLength
    );

    [DllImport(LibraryName)]
    public static extern int resp_header_remove(
        Handle responseHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pName,
        int nameLength
    );
}
