using BuildingBlocks;
using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id):IQuery<DeleteProductResponse>;
public record DeleteProductResponse(Guid Id);
public class DeleteProductHandler(IDocumentSession session) : IQueryHandler<DeleteProductRequest, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await session.Query<Models.Products>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
        {
            throw new KeyNotFoundException($"Product with id {request.Id} not found");
        }
        session.Delete<Models.Products>(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResponse(request.Id);
    }
}