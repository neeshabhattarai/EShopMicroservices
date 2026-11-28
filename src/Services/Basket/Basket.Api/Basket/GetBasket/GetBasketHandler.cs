using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResultHandler>;
public record GetBasketResultHandler(ShoppingCart basket);
public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResultHandler>
{
    public Task<GetBasketResultHandler> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetBasketResultHandler(new ShoppingCart(request.UserName)));
    }
}
