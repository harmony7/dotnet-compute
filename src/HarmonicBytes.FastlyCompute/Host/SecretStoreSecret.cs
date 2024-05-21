using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class SecretStoreSecret(Handle handle)
{
    public Handle Handle { get; } = handle;

    public string GetPlaintext()
    {
        var bytes = new byte[Constants.MaxDictionaryEntryLength];
        var result = Interop.FastlySecretStore.secret_store_plaintext(
            Handle,
            bytes,
            bytes.Length,
            out var bytesWritten
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return Encoding.UTF8.GetString(bytes, 0, bytesWritten);
    }
}
