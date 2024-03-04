namespace NetManager.Core.Exceptions;

public class PlatformNotSupported : Exception
{
    public PlatformNotSupported() : base() { }

    public PlatformNotSupported(string message) : base(message) { }

    public PlatformNotSupported(string message, Exception innerException) : base(message, innerException) { }
}