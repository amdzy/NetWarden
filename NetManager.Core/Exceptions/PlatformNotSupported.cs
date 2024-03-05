namespace NetManager.Core.Exceptions;

public class PlatformNotSupportedException : Exception
{
    public PlatformNotSupportedException() : base("Platform not supported.") { }

    public PlatformNotSupportedException(string message) : base(message) { }

    public PlatformNotSupportedException(string message, Exception innerException) : base(message, innerException) { }
}