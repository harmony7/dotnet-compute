using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute;

public class FastlyException(FastlyError fastlyError) : Exception($"Host Error: {fastlyError}")
{
    public FastlyError FastlyError { get; } = fastlyError;

    public FastlyException(int result) : this(Result.ConvertToFastlyError(result))
    {
    }
}
