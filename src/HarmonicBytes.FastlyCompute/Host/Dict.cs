using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal class Dict(Handle handle)
{
    public Handle Handle { get; } = handle;

    public static Handle GetByName(string dictName)
    {
        var dictNameBytes = Encoding.UTF8.GetBytes(dictName);
        var result = Interop.FastlyDictionary.dictionary_open(
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

    public string GetValueForKey(string key)
    {
        var keyNameBytes = Encoding.UTF8.GetBytes(key);
        var bytes = new byte[Constants.MaxDictionaryEntryLength];
        var result = Interop.FastlyDictionary.dictionary_get(
            Handle,
            keyNameBytes,
            keyNameBytes.Length,
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
