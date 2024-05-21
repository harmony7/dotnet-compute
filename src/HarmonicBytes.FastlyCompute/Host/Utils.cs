using System.Text;

namespace HarmonicBytes.FastlyCompute.Host;

internal static class Utils
{
    public delegate (int endingCursor, int length) ReadAction(
        byte[] buffer,
        int cursor
    );

    public static IEnumerable<string> HostcallReadMultipleStrings(
        ReadAction readAction
    ) {
        var resultStrings = new List<string>();
        var bytes = new byte[Constants.HostcallBufferLength];
        var cursor = 0;
        while (true)
        {
            var (endingCursor, length) = readAction(
                bytes,
                cursor
            );
                
            if (length == 0)
            {
                break;
            }
                    
            var pos = 0;
            while (pos < length)
            {
                var end = Array.IndexOf(bytes, (byte)0, pos);
                if (end == -1)
                {
                    break;
                }
                        
                resultStrings.Add(Encoding.UTF8.GetString(bytes, pos, end - pos));
                pos = end + 1;
            }

            if (endingCursor < 0)
            {
                break;
            }
            cursor = endingCursor;
        }

        return resultStrings;
    }
}
