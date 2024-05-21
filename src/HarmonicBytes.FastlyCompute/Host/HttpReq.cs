using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class HttpReq(Handle handle)
{
    public Handle Handle { get; } = handle;

    public HttpReq(): this(Create())
    {
    }

    public static Handle Create()
    {
        var result = Interop.FastlyHttpReq.req_new(
            out var requestHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }

        return requestHandle;
    }

    public string Method
    {
        get
        {
            var bytes = new byte[Constants.MaxMethodLength + 1];
            bytes[Constants.MaxMethodLength] = 0;
            var result = Interop.FastlyHttpReq.req_method_get(
                Handle,
                bytes,
                bytes.Length,
                out var written
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }
            return Encoding.UTF8.GetString(bytes, 0, written);
        }
        set
        {
            var bytes = Encoding.UTF8.GetBytes(value); 
            var result = Interop.FastlyHttpReq.req_method_set(
                Handle, 
                bytes, 
                bytes.Length
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }
        }
    }

    public string Uri
    {
        get
        {
            var bytes = new byte[Constants.MaxUriLength + 1];
            bytes[Constants.MaxUriLength] = 0;
            var result = Interop.FastlyHttpReq.req_uri_get(
                Handle, 
                bytes, 
                bytes.Length, 
                out var written
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }
            return Encoding.UTF8.GetString(bytes, 0, written);
        }
        set
        {
            var bytes = Encoding.UTF8.GetBytes(value); 
            var result = Interop.FastlyHttpReq.req_uri_set(
                Handle, 
                bytes,
                bytes.Length
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }
        }
    }

    public FastlyHttpVersion HttpVersion
    {
        get
        {
            var result = Interop.FastlyHttpReq.req_version_get(
                Handle, 
                out var httpVersion
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }

            return Result.ConvertToFastlyHttpVersion(httpVersion);
        }
        set
        {
            var httpVersion = Result.ConvertToInt(value);
            var result = Interop.FastlyHttpReq.req_version_set(
                Handle, 
                httpVersion
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }
        }
    }

    public IEnumerable<string> GetHeaderNames()
    {
        return Utils.HostcallReadMultipleStrings(
            (buffer, cursor) =>
            {
                var result = Interop.FastlyHttpReq.req_header_names_get(
                    Handle,
                    buffer,
                    buffer.Length,
                    cursor,
                    out var endingCursor,
                    out var length
                );
                if (result != 0)
                {
                    throw new FastlyException(result);
                }
                return (endingCursor, length);
            }
        );
    }

    public IEnumerable<string> GetHeaderValues(string headerName)
    {
        return Utils.HostcallReadMultipleStrings(
            (buffer, cursor) =>
            {
                var headerNameBytes = Encoding.UTF8.GetBytes(headerName);
                var result = Interop.FastlyHttpReq.req_header_values_get(
                    Handle,
                    headerNameBytes,
                    headerNameBytes.Length,
                    buffer,
                    buffer.Length,
                    cursor,
                    out var endingCursor,
                    out var length
                );
                if (result != 0)
                {
                    throw new FastlyException(result);
                }
                return (endingCursor, length);
            }
        );
    }

    public void InsertHeader(string headerName, string headerValue)
    {
        var headerNameBytes = Encoding.UTF8.GetBytes(headerName);
        var headerValueBytes = Encoding.UTF8.GetBytes(headerValue);
        var result = Interop.FastlyHttpReq.req_header_insert(
            Handle,
            headerNameBytes,
            headerNameBytes.Length,
            headerValueBytes, 
            headerValueBytes.Length
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }

    public void AppendHeader(string headerName, string headerValue)
    {
        var headerNameBytes = Encoding.UTF8.GetBytes(headerName);
        var headerValueBytes = Encoding.UTF8.GetBytes(headerValue);
        var result = Interop.FastlyHttpReq.req_header_append(
            Handle,
            headerNameBytes, 
            headerNameBytes.Length,
            headerValueBytes, 
            headerValueBytes.Length
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }

    public void RemoveHeader(string headerName)
    {
        var bytes = Encoding.UTF8.GetBytes(headerName);
        var result = Interop.FastlyHttpReq.req_header_remove(
            Handle,
            bytes,
            bytes.Length
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }

    public static byte[] GetDownstreamClientIpAddr()
    {
        var bytes = new byte[16];
        var result = Interop.FastlyHttpReq.req_downstream_client_ip_addr_get(
            bytes,
            out var bytesWritten
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return bytes[..bytesWritten];
    }

    public static byte[]? GetDownstreamTlsJa3Md5()
    {
        var bytes = new byte[16];
        var result = Interop.FastlyHttpReq.req_downstream_tls_ja3_md5(
            bytes,
            out var bytesWritten
        );
        if (result == (int)FastlyError.OptionalNone)
        {
            // Viceroy doesn't support this value;
            return null;
        }
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return bytes[..bytesWritten];
    }

    public static string? GetDownstreamTlsCipherOpensslName()
    {
        var bytes = new byte[Constants.HostcallBufferLength];
        var result = Interop.FastlyHttpReq.req_downstream_tls_cipher_openssl_name(
            bytes,
            bytes.Length,
            out var bytesWritten
        );
        if (result == (int)FastlyError.OptionalNone)
        {
            // Viceroy doesn't support this value;
            return null;
        }
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return Encoding.UTF8.GetString(bytes, 0, bytesWritten);
    }

    public static string? GetDownstreamTlsProtocol()
    {
        var bytes = new byte[Constants.HostcallBufferLength];
        var result = Interop.FastlyHttpReq.req_downstream_tls_protocol(
            bytes,
            bytes.Length,
            out var bytesWritten
        );
        if (result == (int)FastlyError.OptionalNone)
        {
            // Viceroy doesn't support this value;
            return null;
        }
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return Encoding.UTF8.GetString(bytes, 0, bytesWritten);
    }

    public static byte[]? GetDownstreamTlsClientCertificate()
    {
        var bytes = new byte[Constants.HostcallBufferLength];
        var result = Interop.FastlyHttpReq.req_downstream_tls_raw_client_certificate(
            bytes,
            bytes.Length,
            out var bytesWritten
        );
        if (result == (int)FastlyError.OptionalNone)
        {
            // Viceroy doesn't support this value;
            return null;
        }
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return bytes[..bytesWritten];
    }

    public static byte[]? GetDownstreamTlsClientHello()
    {
        var bytes = new byte[Constants.HostcallBufferLength];
        var result = Interop.FastlyHttpReq.req_downstream_tls_client_hello(
            bytes,
            bytes.Length,
            out var bytesWritten
        );
        if (result == (int)FastlyError.OptionalNone)
        {
            // Viceroy doesn't support this value;
            return null;
        }
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return bytes[..bytesWritten];
    }

    public void ApplyCacheOverride(CacheOverride cacheOverride)
    {
        var backendNameBytes = cacheOverride.SurrogateKey != null ? Encoding.UTF8.GetBytes(cacheOverride.SurrogateKey) : null;
        var result = Interop.FastlyHttpReq.req_cache_override_v2_set(
            Handle,
            cacheOverride.BuildTag(),
            cacheOverride.TimeToLive ?? 0,
            cacheOverride.StaleWhileRevalidate ?? 0,
            backendNameBytes,
            backendNameBytes?.Length ?? 0
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }

    public void ApplyCacheKey(string cacheKey)
    {
        AppendHeader("fastly-xqd-cache-key", CacheKey.BuildSha256HexString(cacheKey));
    }
}
