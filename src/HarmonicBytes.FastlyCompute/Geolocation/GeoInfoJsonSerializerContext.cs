using System.Text.Json.Serialization;

namespace HarmonicBytes.FastlyCompute.Geolocation;

[JsonSerializable(typeof(GeoInfo))]
internal partial class GeoInfoJsonSerializerContext : JsonSerializerContext;
