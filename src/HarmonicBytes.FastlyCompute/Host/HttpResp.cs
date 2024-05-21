using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class HttpResp(Handle handle)
{
    public Handle Handle { get; } = handle;

    public HttpResp(): this(Create())
    {
    }

    public static Handle Create()
    {
        var result = Interop.FastlyHttpResp.resp_new(
            out var responseHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }

        return responseHandle;
    }

    public ushort Status
    {
        get
        {
            var result = Interop.FastlyHttpResp.resp_status_get(
                Handle,
                out var status
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }

            return status;
        }
        set
        {
            var result = Interop.FastlyHttpResp.resp_status_set(
                Handle, 
                value
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
            var result = Interop.FastlyHttpResp.resp_version_get(
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
            var result = Interop.FastlyHttpResp.resp_version_set(
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
                var result = Interop.FastlyHttpResp.resp_header_names_get(
                    Handle,
                    buffer, buffer.Length,
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
                var result = Interop.FastlyHttpResp.resp_header_values_get(
                    Handle,
                    headerNameBytes,
                    headerNameBytes.Length,
                    buffer,
                    buffer.Length,
                    cursor,
                    out int endingCursor,
                    out int length
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
        var result = Interop.FastlyHttpResp.resp_header_insert(
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
        var result = Interop.FastlyHttpResp.resp_header_append(
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
        var headerNameBytes = Encoding.UTF8.GetBytes(headerName);
        var result = Interop.FastlyHttpResp.resp_header_remove(
            Handle,
            headerNameBytes,
            headerNameBytes.Length
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }

    public void SendDownstream(HttpBody httpBody, bool streaming)
    {
        var result = Interop.FastlyHttpResp.resp_send_downstream(
            Handle,
            httpBody.Handle,
            streaming
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    } 
}
