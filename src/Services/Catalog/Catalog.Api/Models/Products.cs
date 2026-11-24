namespace Catalog.Api.Models;

public class Products
{
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    
    public List<string> Category = new();
    
    public string ImageFile = default!;
    
    
}