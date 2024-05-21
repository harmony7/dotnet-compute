using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute.Platform;

internal static class HttpBodyExtensions
{
    public static HttpBody Write(this HttpBody httpBody, Stream stream)
    {
        while (true)
        {
            var bytes = new byte[8192];
            var readBytes = stream.Read(bytes);
            if (readBytes == 0)
            {
                break;
            }

            httpBody.Write(bytes[..readBytes]);
        }

        return httpBody;
    }
}
