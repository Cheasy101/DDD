using Application.ApplicationLayer.Interfaces;
using Application.ApplicationLayer.Services;
using DDD.APP;
using Infrastructure.InfrastructureLayer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"))
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>((_, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddProjectServices();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    for (int i = 0; i < 100; i++)
    {
        try
        {
            dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();
            break; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attempt {i+1} to migrate database failed. Retrying in 5 seconds...");
            Console.WriteLine(ex.Message);
            System.Threading.Thread.Sleep(1000); // Подождать 5 секунд перед повторной попыткой
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseRouting();

app.MapControllers();

app.Run();