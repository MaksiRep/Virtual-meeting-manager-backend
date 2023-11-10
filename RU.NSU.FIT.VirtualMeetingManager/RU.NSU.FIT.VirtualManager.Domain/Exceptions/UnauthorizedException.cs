namespace RU.NSU.FIT.VirtualManager.Domain.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}