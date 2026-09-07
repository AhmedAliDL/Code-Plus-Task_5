using ECommerce.Application;
using ECommerce.Application.Behavior;
using ECommerce.Application.Events;
using ECommerce.Application.Interfaces;
using ECommerce.DAL.BackgroundServices;
using ECommerce.DAL.Context;
using ECommerce.DAL.Service;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swashbuckle generates the OpenAPI document
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(IAssemblyMarker).Assembly));

builder.Services.AddValidatorsFromAssembly(
    typeof(IAssemblyMarker).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.AddSingleton<
    IBackgroundTaskQueue<ProductAddedToCartEvent>,
    BackgroundTaskQueue<ProductAddedToCartEvent>>();

builder.Services.AddHostedService<
    SendEmailWhenAddingItemsToCartService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("Redis")!
    ));
builder.Services.AddScoped<IProductViewService, ProductViewService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Scalar UI
app.MapScalarApiReference(options =>
{
    options.WithOpenApiRoutePattern(
        "/swagger/{documentName}/swagger.json");
});

app.Run();