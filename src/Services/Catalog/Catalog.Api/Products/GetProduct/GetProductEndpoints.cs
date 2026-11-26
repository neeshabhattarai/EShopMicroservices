namespace Catalog.Api.Products.GetProduct;

public class GetProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductRequest());
            return Results.Ok(result);
        }).WithTags("Products").WithName("GetProducts");
    }
}