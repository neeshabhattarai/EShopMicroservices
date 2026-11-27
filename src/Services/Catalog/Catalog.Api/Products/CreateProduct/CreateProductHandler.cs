using BuildingBlocks;
using BuildingBlocks.CQRS;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, decimal Price,IList<string> Category, string ImageFile):ICommand<CreateProductResponse>;
public record CreateProductResponse(Guid Id, string Name, decimal Price);

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
    }
}
public class CreateProductHandler(IDocumentSession docs,ILogger<CreateProductHandler> logger):ICommandHandler<CreateProductCommand,CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating product {Name}", request.Name);
        var products = new Models.Products
        {
            Name = request.Name,
            Price = request.Price
        };
        docs.Store(products);
        await docs.SaveChangesAsync(cancellationToken);
        return new CreateProductResponse(products.Id, products.Name, products.Price);
        
    }
}

