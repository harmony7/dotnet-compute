using System.Net;
using System.Text.Json;

namespace HarmonicBytes.FastlyCompute.Geolocation;

public static class Geo
{
    public static GeoInfo? GetGeolocationForIpAddress(IPAddress address)
    {
        var geoInfoString = Host.Geo.Lookup(address.GetAddressBytes());
        if (string.IsNullOrWhiteSpace(geoInfoString))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize(geoInfoString, GeoInfo.JsonTypeInfo);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public static GeoInfo? GetGeolocationForIpAddress(string address)
    {
        return GetGeolocationForIpAddress(IPAddress.Parse(address));
    }
}
