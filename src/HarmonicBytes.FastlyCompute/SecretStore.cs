using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute;

using HostSecretStore = Host.SecretStore;

public class SecretStore
{
    private readonly string _name;
    private readonly HostSecretStore _secretStore;

    public SecretStore(string name)
    {
        _name = name;
        if (Instances.TryGetValue(name, out var secretStore))
        {
            _secretStore = secretStore;
            return;
        }

        try
        {
            var handle = HostSecretStore.GetByName(name);
            secretStore = new HostSecretStore(handle);
        }
        catch (FastlyException ex)
        {
            if (ex.FastlyError != FastlyError.BadHandle)
            {
                throw;
            }
            throw new InvalidOperationException("No SecretStore found with name: " + name);
        }

        _secretStore = secretStore;
        Instances[name] = _secretStore;
    }

    private static readonly Dictionary<string, HostSecretStore> Instances = new();
    private static readonly Dictionary<Handle, SecretStoreEntry> Entries = new();

    public SecretStoreEntry this[string key]
    {
        get
        {
            Handle handle;
            try
            {
                handle = _secretStore.GetSecretForKey(key);
            }
            catch (FastlyException ex)
            {
                if (ex.FastlyError != FastlyError.OptionalNone)
                {
                    throw;
                }
                throw new KeyNotFoundException("No Key named " + key + " found in SecretStore " + _name + ".");
            }

            if (Entries.TryGetValue(handle, out var secretStoreEntry))
            {
                return secretStoreEntry;
            }

            secretStoreEntry = new SecretStoreEntry(new SecretStoreSecret(handle), this);
            Entries[handle] = secretStoreEntry;
            return secretStoreEntry;
        }
    }
}
