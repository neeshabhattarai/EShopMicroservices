using BuildingBlocks;
using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.GetProduct;

public record GetProductRequest():IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Models.Products> products);

public class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductRequest, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductRequest request, CancellationToken cancellationToken){
   
    var products = await session.Query<Models.Products>().ToListAsync(cancellationToken);
    return new GetProductResult(products);
    }
}