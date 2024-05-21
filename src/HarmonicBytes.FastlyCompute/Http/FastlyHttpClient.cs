namespace HarmonicBytes.FastlyCompute.Http;

public class FastlyHttpClient: HttpMessageInvoker
{
    public FastlyHttpMessageHandler Handler { get; set;  }

    public FastlyHttpClient(): this(new FastlyHttpMessageHandler()) {
    }

    private FastlyHttpClient(FastlyHttpMessageHandler handler): base(handler)
    {
        Handler = handler;
    }
}
