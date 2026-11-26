using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.DeleteProduct;

public class DeleteProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async ([FromRoute] Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductRequest(id));
            return Results.NoContent();
        }).WithTags("Products").WithName("DeleteProduct");
    }
}