using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class SecretStore(Handle handle)
{
    public Handle Handle { get; } = handle;

    public static Handle GetByName(string dictName)
    {
        var dictNameBytes = Encoding.UTF8.GetBytes(dictName);
        var result = Interop.FastlySecretStore.secret_store_open(
            dictNameBytes,
            dictNameBytes.Length,
            out var handle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return handle;
    }

    public Handle GetSecretForKey(string key)
    {
        var keyNameBytes = Encoding.UTF8.GetBytes(key);
        var result = Interop.FastlySecretStore.secret_store_get(
            Handle,
            keyNameBytes,
            keyNameBytes.Length,
            out var secretStoreSecretHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return secretStoreSecretHandle;
    }
}
