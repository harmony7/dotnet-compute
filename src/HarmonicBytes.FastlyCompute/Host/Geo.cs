using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal static class Geo
{
    public static string Lookup(byte[] address)
    {
        var buffer = new byte[Constants.HostcallBufferLength];
        var result = Interop.FastlyGeo.geo_lookup(
            address,
            address.Length,
            buffer,
            buffer.Length,
            out var written
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return Encoding.UTF8.GetString(buffer, 0, written);
    }
}
