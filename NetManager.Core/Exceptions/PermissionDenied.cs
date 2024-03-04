namespace NetManager.Core.Exceptions;

public class PermissionDeniedException : Exception
{
    public PermissionDeniedException() : base() { }

    public PermissionDeniedException(string message) : base(message) { }

    public PermissionDeniedException(string message, Exception innerException) : base(message, innerException) { }
}