using Org.BouncyCastle.Crypto.Digests;
using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal static class CacheKey
{
    public static string BuildSha256HexString(string input)
    {
        // We use BouncyCastle, because System.Security.Cryptography
        // is currently not supported in .NET for WASI
        // var hashBytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(input));
        
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var myHash = new Sha256Digest();
        myHash.BlockUpdate(inputBytes, 0, inputBytes.Length);
        var hashBytes = new byte[myHash.GetDigestSize()];
        myHash.DoFinal(hashBytes, 0);
        return Convert.ToHexString(hashBytes);
    }
}
