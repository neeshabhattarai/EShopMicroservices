var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);
    c.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    c.AddOpenBehavior(typeof(LoggerGlobal<,>));
});
builder.Services.AddCarter();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapCarter();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
