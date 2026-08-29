using ECommerce.Application;
using ECommerce.Application.Behavior;
using ECommerce.DAL.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
