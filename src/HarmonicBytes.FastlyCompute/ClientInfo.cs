using System.Net;
using HarmonicBytes.FastlyCompute.Geolocation;
using HarmonicBytes.FastlyCompute.Host;
using Geo = HarmonicBytes.FastlyCompute.Geolocation.Geo;

namespace HarmonicBytes.FastlyCompute;

public class ClientInfo
{
    private readonly Lazy<string?> _address;
    private readonly Lazy<GeoInfo?> _geoInfo;
    private readonly Lazy<string?> _tlsJa3Md5;
    private readonly Lazy<string?> _tlsCipherOpensslName;
    private readonly Lazy<string?> _tlsProtocol;
    private readonly Lazy<byte[]?> _tlsClientCertificate;
    private readonly Lazy<byte[]?> _tlsClientHello;

    private ClientInfo()
    {
        _address = new Lazy<string?>(() =>
        {
            var ipAddressBytes = HttpReq.GetDownstreamClientIpAddr();
            var ipAddress = new IPAddress(ipAddressBytes);
            return ipAddress.ToString();
        });

        _geoInfo = new Lazy<GeoInfo?>(() =>
        {
            if (Address == null)
            {
                return null;
            }

            return Geolocation.Geo.GetGeolocationForIpAddress(Address);
        });

        _tlsJa3Md5 = new Lazy<string?>(() =>
        {
            var tlsJa3Md5 = HttpReq.GetDownstreamTlsJa3Md5();
            return tlsJa3Md5 != null ? Convert.ToHexString(tlsJa3Md5) : null;
        });

        _tlsCipherOpensslName = new Lazy<string?>(HttpReq.GetDownstreamTlsCipherOpensslName);
        _tlsProtocol = new Lazy<string?>(HttpReq.GetDownstreamTlsProtocol);
        _tlsClientCertificate = new Lazy<byte[]?>(HttpReq.GetDownstreamTlsClientCertificate);
        _tlsClientHello = new Lazy<byte[]?>(HttpReq.GetDownstreamTlsClientHello);
    }

    public static readonly ClientInfo Default = new();

    public string? Address => _address.Value;
    public GeoInfo? GeoInfo => _geoInfo.Value;
    public string? TlsJa3Md5 => _tlsJa3Md5.Value;
    public string? TlsCipherOpensslName => _tlsCipherOpensslName.Value;
    public string? TlsProtocol => _tlsProtocol.Value;
    public byte[]? TlsClientCertificate => _tlsClientCertificate.Value;
    public byte[]? TlsClientHello => _tlsClientHello.Value;
}
