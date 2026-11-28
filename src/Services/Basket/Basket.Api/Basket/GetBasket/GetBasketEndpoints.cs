
namespace Basket.Api.Basket.GetBasket;

public record GetBasketResult(Guid Id, string UserName, DateTime CreatedAt, IList<ShoppingCartItem> Items);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async ([FromRoute]string userName, ISender sender) =>
        {
            var basket = await sender.Send(new GetBasketQuery(userName));
            Console.WriteLine(basket.basket);
            return Results.Ok(basket.basket.Adapt<GetBasketResult>());

        }).WithName("GetBasket").WithTags("Basket");
    }
}
