using Application.ApplicationLayer.Interfaces;
using Application.ApplicationLayer.Services;
using Domain.Domain_Layer.Interfaces;
using Infrastructure.InfrastructureLayer.Repositories;

namespace DDD.APP;

public static class ServiceExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        return services;
    }
}