using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute;

public class SecretStoreEntry
{
    private readonly Lazy<string> _plaintext;
    public string Plaintext => _plaintext.Value;

    internal SecretStoreEntry(SecretStoreSecret secretStoreSecret, SecretStore secretStore)
    {
        _plaintext = new Lazy<string>(secretStoreSecret.GetPlaintext);
        SecretStore = secretStore;
    }

    public SecretStore SecretStore { get; }

    public override string ToString()
    {
        return Plaintext;
    }
}
