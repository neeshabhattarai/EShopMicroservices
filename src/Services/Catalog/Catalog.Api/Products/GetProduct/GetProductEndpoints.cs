namespace Catalog.Api.Products.GetProduct;
public record GetProductRequest(int Page = 1, int PageSize = 10);
public class GetProductEndpoints :ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, [AsParameters] GetProductRequest request) =>
        {
            var result = await sender.Send(request.Adapt<GetProductCommand>());
            return Results.Ok(result);
        }).WithTags("Products").WithName("GetProducts");
    }
}