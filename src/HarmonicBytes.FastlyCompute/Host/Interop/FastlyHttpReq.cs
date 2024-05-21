using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

internal static class FastlyHttpReq
{
    // HACK: Any library name that P/Invoke generator knows
    private const string LibraryName = "libSystem.Native";

    [DllImport(LibraryName)]
    public static extern int req_new(
        out Handle reqHandle
    );

    [DllImport(LibraryName)]
    public static extern int req_body_downstream_get(
        out Handle pReqHandle,
        out Handle pBodyHandle
    );

    [DllImport(LibraryName)]
    public static extern int req_send(
        Handle reqHandle,
        Handle bodyHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] backend,
        int backendLength,
        out Handle respHandle,
        out Handle repBodyHandle
    );

    [DllImport(LibraryName)]
    public static extern int req_method_get(
        Handle reqHandle,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] method,
        int methodMaxLen,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_method_set(
        Handle reqHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] method,
        int methodLen
    );

    [DllImport(LibraryName)]
    public static extern int req_uri_get(
        Handle reqHandle,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] uri,
        int uriMaxLen,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_uri_set(
        Handle reqHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] uri,
        int uriLen
    );

    [DllImport(LibraryName)]
    public static extern int req_version_get(
        Handle requestHandle,
        out int httpVersion
    );

    [DllImport(LibraryName)]
    public static extern int req_version_set(
        Handle requestHandle,
        int version
    );

    [DllImport(LibraryName)]
    public static extern int req_header_names_get(
        Handle requestHandle,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer,
        int bufferLength,
        int cursor,
        out int endingCursor,
        out int length
    );

    [DllImport(LibraryName)]
    public static extern int req_header_values_get(
        Handle requestHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] buffer,
        int bufferLength,
        int cursor,
        out int endingCursor,
        out int length
    );

    [DllImport(LibraryName)]
    public static extern int req_header_insert(
        Handle requestHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] value,
        int valueLength
    );

    [DllImport(LibraryName)]
    public static extern int req_header_append(
        Handle requestHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] value,
        int valueLength
    );

    [DllImport(LibraryName)]
    public static extern int req_header_remove(
        Handle requestHandle,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] name,
        int nameLength
    );

    [DllImport(LibraryName)]
    public static extern int req_redirect_to_grip_proxy(
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pBackendName,
        int backendNameLength
    );

    [DllImport(LibraryName)]
    public static extern int req_downstream_client_ip_addr_get(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] byte[] octets,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_downstream_tls_ja3_md5(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] byte[] octets,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_downstream_tls_cipher_openssl_name(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] tlsCipherOpensslName,
        int bufferSize,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_downstream_tls_protocol(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] tlsProtocol,
        int bufferSize,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_downstream_tls_raw_client_certificate(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] tlsRawClientCertificate,
        int bufferSize,
        out int bytesWritten
    );

    [DllImport(LibraryName)]
    public static extern int req_downstream_tls_client_hello(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] tlsClientHello,
        int bufferSize,
        out int bytesWritten
    );
    
    [DllImport(LibraryName)]
    public static extern int req_cache_override_v2_set(
        Handle requestHandle,
        uint tag,
        uint ttl,
        uint staleWhileRevalidate,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[]? surrogateKeyBytes,
        int surrogateKeyLength
    );
}
