namespace TeamMemberships.Service.Errors;

public class BadRequestError : Error
{
    public BadRequestError()
    {
    }

    public BadRequestError(string? message) : base(message)
    {
    }

    public BadRequestError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}