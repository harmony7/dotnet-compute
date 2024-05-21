namespace HarmonicBytes.FastlyCompute;

public enum FastlyError
{
    // Unknown error value.
    // It should be an internal error if this is returned.
    UnknownError = 0,

    // Generic error value.
    // This means that some unexpected error occurred during a hostcall.
    GenericError = 1,

    // Invalid argument.
    InvalidArgument = 2,

    // Invalid handle.
    // Thrown when a handle is not valid. E.G. No dictionary exists with the given name.
    BadHandle = 3,

    // Buffer length error.
    // Thrown when a buffer is too long.
    BufferLen = 4,

    // Unsupported operation error.
    // This error is thrown when some operation cannot be performed, because it is not supported.
    Unsupported = 5,

    // Alignment error.
    // This is thrown when a pointer does not point to a properly aligned slice of memory.
    BadAlign = 6,

    // Invalid HTTP error.
    // This can be thrown when a method, URI, header, or status is not valid. This can also
    // be thrown if a message head is too large.
    HttpInvalid = 7,

    // HTTP user error.
    // This is thrown in cases where user code caused an HTTP error. For example, attempt to send
    // a 1xx response code, or a request with a non-absolute URI. This can also be caused by
    // an unexpected header: both `content-length` and `transfer-encoding`, for example.
    HttpUser = 8,

    // HTTP incomplete message error.
    // This can be thrown when a stream ended unexpectedly.
    HttpIncomplete = 9,

    // A `None` error.
    // This status code is used to indicate when an optional value did not exist, as opposed to
    // an empty value.
    // Note, this value should no longer be used, as we have explicit optional types now.
    OptionalNone = 10,

    // Message head too large.
    HttpHeadTooLarge = 11,

    // Invalid HTTP status.
    HttpInvalidStatus = 12,

    // Limit exceeded
    // 
    // This is returned when an attempt to allocate a resource has exceeded the maximum number of
    // resources permitted. For example, creating too many response handles.
    LimitExceeded = 13,
}
