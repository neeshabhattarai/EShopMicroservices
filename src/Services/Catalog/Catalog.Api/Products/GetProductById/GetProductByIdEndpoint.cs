using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductById;

public class GetProductByIdEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async ([FromRoute]Guid id, ISender sender) =>
        {
            try
            {  
            var result = await sender.Send(new GetProductByIdRequest(id));
            return Results.Ok(result);
            }
            catch (Exception e)
            {
             return Results.NotFound(e.Message);
            }
        }).WithTags("Products").WithName("GetProductById");
    }
}
