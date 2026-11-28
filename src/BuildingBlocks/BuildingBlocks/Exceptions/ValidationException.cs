namespace BuildingBlocks.Exceptions;

public class ValidationException:System.Exception
{

    public ValidationException(string message, string details ) : base($"{message} - {details}")
    {
    }

    public ValidationException() : base()
    {
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }


}
