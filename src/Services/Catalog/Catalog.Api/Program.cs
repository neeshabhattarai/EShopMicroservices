using Catalog.Api.Products.CreateProduct;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR((c) =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    c.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidatorBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapCarter();
app.UseExceptionHandler(ex =>
{
    ex.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception==null)
        {
            return;
        }
        var problem = new ProblemDetails
        {
            Title = exception.GetType().Name,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message
        };
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);

    });
});
app.Run();
