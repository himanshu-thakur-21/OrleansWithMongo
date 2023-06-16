using OrleansWithMongo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();
builder.Host.ConfigureOrleans(builder.Configuration);

var app = builder.Build();

app.RegisterPipelines();

app.MapControllers();

app.Run();
