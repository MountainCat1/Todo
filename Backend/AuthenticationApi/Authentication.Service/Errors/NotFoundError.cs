namespace Authentication.Service.Errors;

public class NotFoundError : Error
{
    public NotFoundError()
    {
    }

    public NotFoundError(string? message) : base(message)
    {
    }

    public NotFoundError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}