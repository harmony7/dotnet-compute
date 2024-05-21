namespace HarmonicBytes.FastlyCompute.Host;

internal class Response(HttpResp httpResp, HttpBody httpBody)
{
    public HttpResp HttpResp { get; } = httpResp;
    public HttpBody HttpBody { get; } = httpBody;

    public Response(): this(new HttpResp(), new HttpBody())
    {
    }

    public void SendDownstream(bool streaming)
    {
        HttpResp.SendDownstream(HttpBody, streaming);
    }
}
