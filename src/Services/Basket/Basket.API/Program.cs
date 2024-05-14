using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Marten;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(option =>
    {
        option.Connection(builder.Configuration.GetConnectionString("Database")!);
        option.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();


app.MapCarter();
app.UseExceptionHandler(option=>{});
app.Run();