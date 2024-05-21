using HarmonicBytes.FastlyCompute.Platform;
using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute;

public static class Invocation
{
    public static HttpRequestMessage GetRequest()
    {
        var request = Request.FromBodyDownstream();
        return request.AsHttpRequestMessage();
    }

    public static ClientInfo GetClientInfo()
    {
        return ClientInfo.Default;
    }

    public static void HandoffFanout(string backendName)
    {
        Request.RedirectToGripProxy(backendName);
    }

    public static void SendResponse(HttpResponseMessage httpResponseMessage)
    {
        var response = httpResponseMessage.AsHostResponse();
        response.SendDownstream(false);
    }
}
