namespace RU.NSU.FIT.VirtualManager.Domain.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}