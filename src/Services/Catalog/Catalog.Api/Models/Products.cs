namespace Catalog.Api.Models;

public class Products
{
    public Guid Id { get; set; }=Guid.NewGuid();
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; } =default!;
    public decimal Price { get; set; } 
    
    public List<string> Category = new();
    
    public string ImageFile = default!;
    
    
}