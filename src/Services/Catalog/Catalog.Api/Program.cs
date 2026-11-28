var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddMediatR((c) =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    c.AddOpenBehavior(typeof(LoggerGlobal<,>));
});
builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions().InitializeWith<SeedData>();
builder.Services.AddExceptionHandler<CustomHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Default")!);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapCarter();
// app.UseExceptionHandler(ex =>
// {
// ex.Run(async context =>
// {
//     var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//     if (exception==null)
//     {
//         return;
//     }
//     var problem = new ProblemDetails
//     {
//         Title = exception.GetType().Name,
//         Status = StatusCodes.Status500InternalServerError,
//         Detail = exception.Message
//     };
//     context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//     context.Response.ContentType = "application/problem+json";
//     await context.Response.WriteAsJsonAsync(problem);

// });
// });
app.Run();
