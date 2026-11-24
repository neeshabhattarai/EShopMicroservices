using BuildingBlocks;
using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, decimal Price,IList<string> Category, string ImageFile):ICommand<CreateProductResponse>;
public record CreateProductResponse(Guid Id, string Name, decimal Price);
public class CreateProductHandler:ICommandHandler<CreateProductCommand,CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var products = new Models.Products
        {
            Name = request.Name,
            Price = request.Price
        };
        return new CreateProductResponse(Guid.NewGuid(), products.Name, products.Price);
        
    }
}

