
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.UpdateProduct;

public class UpdateProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/products/{id}", async ([FromRoute]Guid id, [FromBody]string name, IMediator mediator) =>
        {
            var result = await mediator.Send(new UpdateProductCommand(id, name));
            return result.IsSuccess ? Results.Ok("Product updated successfully") : Results.BadRequest("Product not updated");
        }).WithTags("Products").WithName("UpdateProduct");
    }
}
