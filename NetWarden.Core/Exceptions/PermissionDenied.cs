namespace NetWarden.Core.Exceptions;

public class PermissionDeniedException : Exception
{
    public PermissionDeniedException() : base("Permission Denied, Make sure you have enough permissions to run.") { }

    public PermissionDeniedException(string message) : base(message) { }

    public PermissionDeniedException(string message, Exception innerException) : base(message, innerException) { }
}