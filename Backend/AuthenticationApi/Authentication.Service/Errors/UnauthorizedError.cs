namespace Authentication.Service.Errors;

public class UnauthorizedError : Error
{
    public UnauthorizedError()
    {
    }

    public UnauthorizedError(string? message) : base(message)
    {
    }

    public UnauthorizedError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}