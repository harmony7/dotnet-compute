using System.Net;
using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute.Platform;

internal static class Convert
{
    public static HttpRequestMessage AsHttpRequestMessage(this Request hostRequest)
    {
        var method = HttpMethod.Parse(hostRequest.HttpReq.Method);

        var httpRequestMessage = new HttpRequestMessage(method, hostRequest.HttpReq.Uri);
        httpRequestMessage.Version = hostRequest.HttpReq.HttpVersion.AsVersion();

        if (method != HttpMethod.Get && method != HttpMethod.Head)
        {
            var stream = new HttpBodyStream(hostRequest.HttpBody);
            httpRequestMessage.Content = new StreamContent(stream);
        }

        foreach (var header in hostRequest.HttpReq.GetHeaderNames())
        {
            try
            {
                httpRequestMessage.Headers.Add(header, hostRequest.HttpReq.GetHeaderValues(header));
            }
            catch (InvalidOperationException)
            {
                try
                {
                    httpRequestMessage.Content?.Headers.Add(header, hostRequest.HttpReq.GetHeaderValues(header));
                }
                catch (InvalidOperationException)
                {
                    // Just skip this header
                }
            }
        }

        return httpRequestMessage;
    }

    public static Request AsHostRequest(this HttpRequestMessage httpRequestMessage)
    {
        var hostRequest = new Request
        {
            HttpReq =
            {
                Method = httpRequestMessage.Method.ToString(),
                Uri = httpRequestMessage.RequestUri?.ToString() ?? "",
                HttpVersion = httpRequestMessage.Version.AsFastlyHttpVersion()
            }
        };

        foreach (var kv in httpRequestMessage.Headers)
        {
            foreach (var v in kv.Value)
            {
                hostRequest.HttpReq.AppendHeader(kv.Key, v);
            }
        }
        if (httpRequestMessage.Content != null)
        {
            foreach (var kv in httpRequestMessage.Content.Headers)
            {
                foreach (var v in kv.Value)
                {
                    hostRequest.HttpReq.AppendHeader(kv.Key, v);
                }
            }

            var stream = httpRequestMessage.Content.ReadAsStream();

            hostRequest.HttpBody.Write(stream);
        }

        return hostRequest;
    }

    public static HttpResponseMessage AsHttpResponseMessage(this Response hostResponse)
    {
        var httpResponseMessage = new HttpResponseMessage((HttpStatusCode)hostResponse.HttpResp.Status);
        httpResponseMessage.Version = hostResponse.HttpResp.HttpVersion.AsVersion();
        httpResponseMessage.Content = new StreamContent(new HttpBodyStream(hostResponse.HttpBody));

        foreach (var header in hostResponse.HttpResp.GetHeaderNames())
        {
            try
            {
                httpResponseMessage.Headers.Add(header, hostResponse.HttpResp.GetHeaderValues(header));
            }
            catch (InvalidOperationException)
            {
                try
                {
                    httpResponseMessage.Content.Headers.Add(header, hostResponse.HttpResp.GetHeaderValues(header));
                }
                catch (InvalidOperationException)
                {
                    // Just skip this header
                }
            }
        }

        return httpResponseMessage;
    }

    public static Response AsHostResponse(this HttpResponseMessage httpResponseMessage)
    {
        var hostResponse = new Response
        {
            HttpResp =
            {
                Status = (ushort)httpResponseMessage.StatusCode,
                HttpVersion = httpResponseMessage.Version.AsFastlyHttpVersion(),
            }
        };
        
        foreach (var kv in httpResponseMessage.Headers)
        {
            foreach (var v in kv.Value)
            {
                hostResponse.HttpResp.AppendHeader(kv.Key, v);
            }
        }

        foreach (var kv in httpResponseMessage.Content.Headers)
        {
            foreach (var v in kv.Value)
            {
                hostResponse.HttpResp.AppendHeader(kv.Key, v);
            }
        }
        
        var stream = httpResponseMessage.Content.ReadAsStream();

        hostResponse.HttpBody.Write(stream);

        return hostResponse;
    }

    public static FastlyHttpVersion AsFastlyHttpVersion(this Version version)
    {
        if (version == new Version(0, 9))
        {
            return FastlyHttpVersion.Http09;
        }
        if (version == HttpVersion.Version10)
        {
            return FastlyHttpVersion.Http10;
        }

        if (version == HttpVersion.Version11)
        {
            return FastlyHttpVersion.Http11;
        }

        if (version == HttpVersion.Version20)
        {
            return FastlyHttpVersion.H2;
        }
        // NOTE: This looks suspicious, but js-compute-runtime
        // does this using this logic too
        return FastlyHttpVersion.H3;
    }

    public static Version AsVersion(this FastlyHttpVersion fastlyHttpVersion)
    {
        return fastlyHttpVersion switch
        {
            FastlyHttpVersion.Http09 => new Version(0, 9),
            FastlyHttpVersion.Http10 => HttpVersion.Version10,
            FastlyHttpVersion.Http11 => HttpVersion.Version11,
            FastlyHttpVersion.H2 => HttpVersion.Version20,
            FastlyHttpVersion.H3 => HttpVersion.Version30,
            _ => HttpVersion.Unknown,
        };
    }
}
