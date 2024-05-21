using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute;

public class ConfigStore
{
    private string _name;
    private Dict _dict; 

    public ConfigStore(string name)
    {
        _name = name;
        if (Instances.TryGetValue(name, out var dict))
        {
            _dict = dict;
            return;
        }

        try
        {
            var handle = Dict.GetByName(name);
            dict = new Dict(handle);
        }
        catch (FastlyException ex)
        {
            if (ex.FastlyError != FastlyError.BadHandle)
            {
                throw;
            }
            throw new InvalidOperationException("No ConfigStore found with name: " + name);
        }

        _dict = dict;
        Instances[name] = _dict;
    }

    private static readonly Dictionary<string, Dict> Instances = new();

    public string this[string key]
    {
        get
        {
            string value;
            try
            {
                value = _dict.GetValueForKey(key);
            } 
            catch (FastlyException ex)
            {
                if (ex.FastlyError != FastlyError.OptionalNone)
                {
                    throw;
                }
                throw new KeyNotFoundException("No Key named " + key + " found in ConfigStore " + _name + ".");
            }

            return value;
        }
    }
}
