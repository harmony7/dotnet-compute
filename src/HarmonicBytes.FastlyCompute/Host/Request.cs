using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class Request(HttpReq httpReq, HttpBody httpBody)
{
    public HttpReq HttpReq { get; } = httpReq;
    public HttpBody HttpBody { get; } = httpBody;

    public Request(): this(new HttpReq(), new HttpBody())
    {
    }

    public static Request FromBodyDownstream()
    {
        var result = Interop.FastlyHttpReq.req_body_downstream_get(
            out var requestHandle,
            out var bodyHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }

        return new Request(new HttpReq(requestHandle), new HttpBody(bodyHandle));
    }

    public Response Send(string backendName)
    {
        var backendNameBytes = Encoding.UTF8.GetBytes(backendName);
        var result = Interop.FastlyHttpReq.req_send(
            HttpReq.Handle,
            HttpBody.Handle,
            backendNameBytes,
            backendNameBytes.Length,
            out var responseHandle,
            out var responseBodyHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }

        return new Response(new HttpResp(responseHandle), new HttpBody(responseBodyHandle));
    }

    public static void RedirectToGripProxy(string backendName)
    {
        var backendNameBytes = Encoding.UTF8.GetBytes(backendName);
        var result = Interop.FastlyHttpReq.req_redirect_to_grip_proxy(
            backendNameBytes,
            backendNameBytes.Length
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }
}
