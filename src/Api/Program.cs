using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WorldAlerts.Application.Extensions;
using WorldAlerts.Infrastructure.Data;
using WorldAlerts.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "World Alerts API",
        Version = "v1",
    });
});

// Add Application services
builder.Services.AddApplicationServices();

// Add Infrastructure services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddInfrastructureServices(connectionString);

var app = builder.Build();

// Lightweight database initialization guard
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WorldAlertsDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "World Alerts API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
