using BuildingBlocks;
using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdRequest(Guid Id):IQuery<GetProductByIdResponse>;
public record GetProductByIdResponse(Models.Products Product);
public class GetProductByIdHanlder(IDocumentSession session):IQueryHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await session.Query<Models.Products>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {request.Id} not found");
        }
        return new GetProductByIdResponse(product);
    }
}
