using HarmonicBytes.FastlyCompute.Host;
using HarmonicBytes.FastlyCompute.Platform;

namespace HarmonicBytes.FastlyCompute;

public class KVStore
{
    private readonly ObjectStore _objectStore;

    public KVStore(string name)
    {
        if (Instances.TryGetValue(name, out var objectStore))
        {
            _objectStore = objectStore;
            return;
        }

        try
        {
            var handle = ObjectStore.GetByName(name);
            objectStore = new ObjectStore(handle);
        }
        catch (FastlyException ex)
        {
            if (ex.FastlyError != FastlyError.BadHandle)
            {
                throw;
            }
            throw new InvalidOperationException("No KVStore found with name: " + name);
        }

        _objectStore = objectStore;
        Instances[name] = _objectStore;
    }

    private static readonly Dictionary<string, ObjectStore> Instances = new();

    public Stream this[string key]
    {
        get
        {
            var entryHandle = _objectStore.GetEntryForKey(key);
            // a ZERO return value means no entry was found for that key.
            // We create an brand new empty body for that case.
            var entryBody = entryHandle != 0 ? new HttpBody(entryHandle) : new HttpBody();
            return new HttpBodyStream(entryBody);
        }
        set
        {
            var entryBody = new HttpBody();
            entryBody.Write(value);
            _objectStore.SetEntryForKey(key, entryBody.Handle);
        }
    }
}
