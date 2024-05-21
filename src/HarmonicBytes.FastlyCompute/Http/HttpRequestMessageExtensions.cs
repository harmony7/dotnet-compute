namespace HarmonicBytes.FastlyCompute.Http;

public static class HttpRequestMessageExtensions
{
    public static FastlyHttpOptions GetFastlyOptions(this HttpRequestMessage httpRequestMessage)
    {
        if (httpRequestMessage.Options.TryGetValue(FastlyHttpOptions.OptionsKey, out var fastlyHttpOptions))
        {
            return fastlyHttpOptions;
        }
        fastlyHttpOptions = new FastlyHttpOptions();
        httpRequestMessage.SetFastlyOptions(fastlyHttpOptions);
        return fastlyHttpOptions;
    }

    public static void SetFastlyOptions(this HttpRequestMessage httpRequestMessage, FastlyHttpOptions fastlyHttpOptions)
    {
        httpRequestMessage.Options.Set(FastlyHttpOptions.OptionsKey, fastlyHttpOptions);
    }
}
