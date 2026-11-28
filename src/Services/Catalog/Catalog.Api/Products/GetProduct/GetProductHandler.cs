using BuildingBlocks;
using BuildingBlocks.CQRS;
using Marten.Pagination;

namespace Catalog.Api.Products.GetProduct;

public record GetProductCommand(int Page = 1, int PageSize = 10):IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Models.Products> products);

public class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductCommand, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Models.Products>().ToPagedListAsync(request.Page, request.PageSize, cancellationToken);
        return new GetProductResult(products);
    }
}