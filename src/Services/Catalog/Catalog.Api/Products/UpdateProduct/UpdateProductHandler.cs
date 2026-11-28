using System.Windows.Input;
using BuildingBlocks;
using BuildingBlocks.CQRS;
using Catalog.Api.Exception;

namespace Catalog.Api.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name) :ICommand<UpdateProductResponse>;
public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductHandler(IDocumentSession docs) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await docs.LoadAsync<Models.Products>(request.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(typeof(Models.Products).Name, request.Id.ToString());
        }
        product.Name = request.Name;
        docs.Store(product);
        Console.WriteLine($"After update: {product.Name}");
        await docs.SaveChangesAsync(cancellationToken);
        return new UpdateProductResponse(IsSuccess: true);
    }
}
