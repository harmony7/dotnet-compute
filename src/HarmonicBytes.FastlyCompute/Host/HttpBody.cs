namespace HarmonicBytes.FastlyCompute.Host;

internal class HttpBody(Handle handle)
{
    public Handle Handle { get; } = handle;

    public HttpBody(): this(Create())
    {
    }

    public static Handle Create()
    {
        var result = Interop.FastlyHttpBody.body_new(
            out var bodyHandle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }

        return bodyHandle;
    }

    public byte[] Read(int size)
    {
        var buffer = new byte[size];
        var result = Interop.FastlyHttpBody.body_read(
            Handle,
            buffer,
            buffer.Length,
            out var written
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return buffer[..written];
    }

    public int Write(byte[] bytes, BodyWriteEnd bodyWriteEnd = BodyWriteEnd.Back)
    {
        var result = Interop.FastlyHttpBody.body_write(
            Handle,
            bytes,
            bytes.Length,
            (int)bodyWriteEnd,
            out var written
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
        return written;
    }

    public void Close()
    {
        var result = Interop.FastlyHttpBody.body_close(
            Handle
        );
        if (result != 0)
        {
            throw new FastlyException(result);
        }
    }
}
