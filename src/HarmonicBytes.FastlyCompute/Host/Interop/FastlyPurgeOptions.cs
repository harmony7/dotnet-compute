using System.Runtime.InteropServices;

namespace HarmonicBytes.FastlyCompute.Host.Interop;

[StructLayout(LayoutKind.Sequential)]  
internal ref struct FastlyPurgeOptions
{
    public IntPtr pBuffer;
    public int bufferLength;
    public ref int bytesWritten;
}
