namespace Basket.Api.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }=Guid.NewGuid();

    public string UserName { get; set; }=string.Empty;
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

    public IList<ShoppingCartItem> Items { get; set; } = [];

    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    public ShoppingCart()
    {
    }
}
