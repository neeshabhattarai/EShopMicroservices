namespace Catalog.Api.Exception;

public class ProductNotFoundException : System.Exception
{
    public ProductNotFoundException(string message) : base(message)
    {
    }

    public ProductNotFoundException() : base("Product not found")
    {
    }

    public ProductNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
