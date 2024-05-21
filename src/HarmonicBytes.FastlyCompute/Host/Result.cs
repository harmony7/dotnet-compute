namespace HarmonicBytes.FastlyCompute.Host;

internal static class Result
{
    public static FastlyError ConvertToFastlyError(int result)
    {
        return result switch
        {
            1 => FastlyError.GenericError,
            2 => FastlyError.InvalidArgument,
            3 => FastlyError.BadHandle,
            4 => FastlyError.BufferLen,
            5 => FastlyError.Unsupported,
            6 => FastlyError.BadAlign,
            7 => FastlyError.HttpInvalid,
            8 => FastlyError.HttpUser,
            9 => FastlyError.HttpIncomplete,
            10 => FastlyError.OptionalNone,
            11 => FastlyError.HttpHeadTooLarge,
            12 => FastlyError.HttpInvalidStatus,
            13 => FastlyError.LimitExceeded,
            _ => FastlyError.UnknownError
        };
    }

    public static FastlyHttpVersion ConvertToFastlyHttpVersion(int httpVersion)
    {
        return httpVersion switch
        {
            0 => FastlyHttpVersion.Http09,
            1 => FastlyHttpVersion.Http10,
            2 => FastlyHttpVersion.Http11,
            3 => FastlyHttpVersion.H2,
            _ => FastlyHttpVersion.H3,
        };
    }

    public static int ConvertToInt(FastlyHttpVersion fastlyHttpVersion)
    {
        return fastlyHttpVersion switch
        {
            FastlyHttpVersion.Http09 => 0,
            FastlyHttpVersion.Http10 => 1,
            FastlyHttpVersion.Http11 => 2,
            FastlyHttpVersion.H2 => 3,
            _ => 4,
        };
    }
}
