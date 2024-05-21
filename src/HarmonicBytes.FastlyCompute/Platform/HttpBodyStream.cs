using HarmonicBytes.FastlyCompute.Host;

namespace HarmonicBytes.FastlyCompute.Platform;

internal class HttpBodyStream(HttpBody body) : Stream
{
    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var bytes = body.Read(count);
        bytes.CopyTo(buffer, offset);
        return bytes.Length;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        var bytes = buffer.AsSpan(offset, count);
        body.Write(bytes.ToArray());
    }

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => throw new NotSupportedException();
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }
}
