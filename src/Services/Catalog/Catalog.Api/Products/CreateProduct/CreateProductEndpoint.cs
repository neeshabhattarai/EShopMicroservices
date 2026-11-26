
namespace Catalog.Api.Products.CreateProduct;

public record CreateProductRequest (string name, decimal price , List<string> category);
public record CreateResponse(Guid id);
public class CreateProductEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Products", async (CreateProductRequest request, ISender sender) =>
        {
            var request1 = request.Adapt<CreateProductCommand>();
           var results= await sender.Send(request1);
            return Results.Created(new Uri("/Products/" + results.Id, UriKind.Relative), new { request1.Name });
        }).WithTags("Products").WithName("CreateProduct");
        app.MapGet("/", () =>
        {
            return Results.Ok("Hello World!");
        }).WithTags("TestApi").WithName("GetTestApi");
    }
    
}