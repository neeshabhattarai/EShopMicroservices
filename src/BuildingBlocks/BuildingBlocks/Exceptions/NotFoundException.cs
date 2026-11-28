namespace BuildingBlocks.Exceptions;

public class NotFoundException: System.Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public NotFoundException() : base()
    {
    }
    public NotFoundException(string key, string id) : base($"Entity {key} with id {id} was not found")
    {
    }

}
