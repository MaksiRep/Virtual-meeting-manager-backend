namespace RU.NSU.FIT.VirtualManager.Domain.Exceptions;

public class InternalServerException : BaseException
{
    public InternalServerException(string message) : base(message)
    {
    }
}