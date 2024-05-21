using HarmonicBytes.FastlyCompute.Platform;

namespace HarmonicBytes.FastlyCompute.Http;

public class FastlyHttpMessageHandler: HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Send(request, cancellationToken));
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri == null)
        {
            throw new InvalidOperationException("Send() called with null request.RequestUri.");
        }

        var fastlyOptions = request.GetFastlyOptions();
        if (string.IsNullOrWhiteSpace(fastlyOptions.Backend))
        {
            throw new InvalidOperationException("Send() called with no Backend set.");
        }

        var hostReq = request.AsHostRequest();

        if (fastlyOptions.CacheOverride != null)
        {
            hostReq.HttpReq.ApplyCacheOverride(fastlyOptions.CacheOverride);
        }

        if (fastlyOptions.CacheKey != null)
        {
            hostReq.HttpReq.ApplyCacheKey(fastlyOptions.CacheKey);
        }
        
        var hostResp = hostReq.Send(fastlyOptions.Backend);

        var response = hostResp.AsHttpResponseMessage();

        return response;
    }
}
