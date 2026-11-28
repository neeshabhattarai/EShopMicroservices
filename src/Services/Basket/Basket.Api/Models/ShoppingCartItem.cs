
namespace Basket.Api.Models;

public class ShoppingCartItem
{
    [Key]
    public Guid Id { get; set; }=Guid.NewGuid();
    public string ProductId { get; set; }=string.Empty;
    public string ProductName { get; set; }=string.Empty;
    public decimal Price { get; set; }=default!;
    public int Quantity { get; set; }=default!;
    public string Color { get; set; }=string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public Guid ShoppingCartId { get; set; }=default!;

}
