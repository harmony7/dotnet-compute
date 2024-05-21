using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HarmonicBytes.FastlyCompute.Geolocation;

public class GeoInfo
{
    [JsonPropertyName("as_name")]
    public string? AutonomousSystemName { get; set; }

    [JsonPropertyName("as_number")]
    public int? AutonomousSystemNumber { get; set; }

    [JsonPropertyName("area_code")]
    public int? AreaCode { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("conn_speed")]
    public string? ConnectionSpeed { get; set; }

    [JsonPropertyName("conn_type")]
    public string? ConnectionType { get; set; }

    [JsonPropertyName("continent")]
    public string? Continent { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("country_code3")]
    public string? CountryCodeAlpha3 { get; set; }

    [JsonPropertyName("country_name")]
    public string? CountryName { get; set; }

    [JsonPropertyName("gmt_offset")]
    public int? GmtOffset { get; set; }

    [JsonPropertyName("latitude")]
    public float? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public float? Longitude { get; set; }

    [JsonPropertyName("metro_code")]
    public int? MetroCode { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("proxy_description")]
    public string? ProxyDescription { get; set; }

    [JsonPropertyName("proxy_type")]
    public string? ProxyType { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("utc_offset")]
    public int? UtcOffset { get; set; }

    internal static readonly JsonTypeInfo<GeoInfo> JsonTypeInfo = GeoInfoJsonSerializerContext.Default.GeoInfo;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonTypeInfo);
    }
}
