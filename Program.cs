using DDD.APP.ApplicationLayer.Interfaces;
using DDD.APP.ApplicationLayer.Services;
using DDD.APP.Domain_Layer.Interfaces;
using DDD.APP.Infrastructure_Layer.Persistence;
using DDD.APP.Infrastructure_Layer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Specify the path to the settings files
builder.Configuration
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"))
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseRouting();

// Map controllers directly at the top level
app.MapControllers();

app.Run();