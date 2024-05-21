using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class ObjectStore(Handle handle)
{
    public Handle Handle { get; } = handle;

    public static Handle GetByName(string name)
    {
        var kvStoreNameBytes = Encoding.UTF8.GetBytes(name);
        var result = Interop.FastlyObjectStore.object_store_open(
            kvStoreNameBytes,
            kvStoreNameBytes.Length,
            out var handle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }

        return handle;
    }

    public Handle GetEntryForKey(string key)
    {
        var keyNameBytes = ParseAndValidateKey(key);
        var result = Interop.FastlyObjectStore.object_store_lookup(
            Handle,
            keyNameBytes,
            keyNameBytes.Length,
            out var entryBodyHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return entryBodyHandle;
    }

    public void SetEntryForKey(string key, Handle entryBodyHandle)
    {
        var keyNameBytes = ParseAndValidateKey(key);
        var result = Interop.FastlyObjectStore.object_store_insert(
            Handle,
            keyNameBytes,
            keyNameBytes.Length,
            entryBodyHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }

    private static byte[] ParseAndValidateKey(string key)
    {
        if (key is "." or "..")
        {
            throw new FormatException("KVStore key can not be '.' or '..'");
        }

        const string acmeChallenge = ".well-known/acme-challenge/";
        if (key.StartsWith(acmeChallenge))
        {
            throw new FormatException("KVStore key can not start with .well-known/acme-challenge/");
        }

        var keyNameBytes = Encoding.UTF8.GetBytes(key);
        if (keyNameBytes.Length < 1)
        {
            throw new FormatException("KVStore key can not be an empty string");
        }
        if (keyNameBytes.Length > 1024)
        {
            // If the converted string has a length of more than 1024 then we throw an Error
            // because KVStore Keys have to be less than 1025 characters.
            throw new FormatException("KVStore key can not be more than 1024 characters");
        }

        foreach (var keyByte in keyNameBytes)
        {
            var invalid = keyByte switch
            {
                (byte)'#' or (byte)'?' or (byte)'*' or (byte)'[' or (byte)']' => keyByte.ToString(),
                (byte)'\r' => "carriage return",
                (byte)'\n' => "newline",
                _ => null
            };
            if (invalid != null)
            {
                throw new FormatException($"KVStore key can not contain {invalid} character");
            }
        }

        return keyNameBytes;
    }
}
