using System.Runtime.InteropServices;
using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal static class Purge
{
    public static string PurgeSurrogateKey(string surrogateKey, bool soft)
    {
        var surrogateKeyBytes = Encoding.UTF8.GetBytes(surrogateKey);

        var purgeOptionsMask = Interop.PurgeOptionsMask.ReturnBuffer;
        if (soft)
        {
            purgeOptionsMask |= Interop.PurgeOptionsMask.SoftPurge;
        }

        var pBuffer = Marshal.AllocCoTaskMem(Constants.HostcallBufferLength);
        try
        {
            var bytesWritten = 0;
            var fastlyPurgeOptions = new Interop.FastlyPurgeOptions
            {
                pBuffer = pBuffer,
                bufferLength = Constants.HostcallBufferLength,
                bytesWritten = ref bytesWritten,
            };
            var result = Interop.FastlyPurge.purge_surrogate_key(
                surrogateKeyBytes,
                surrogateKeyBytes.Length,
                (uint)purgeOptionsMask,
                ref fastlyPurgeOptions
            );
            if (result != 0)
            {
                throw new FastlyException(result);
            }

            return Marshal.PtrToStringAnsi(pBuffer, bytesWritten);
        }
        finally
        {
            Marshal.FreeCoTaskMem(pBuffer);
        }
    }
}
