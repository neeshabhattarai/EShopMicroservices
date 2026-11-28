using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exception;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string key, string id) : base(key,id)
    {
    }

    public ProductNotFoundException(string message="Product not found") : base(message)
    {
    }

    public ProductNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }

    public ProductNotFoundException() : base()
    {
    }
}
