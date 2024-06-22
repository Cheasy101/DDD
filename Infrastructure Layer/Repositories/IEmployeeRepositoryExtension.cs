using DDD.APP.Domain_Layer.Interfaces;

namespace DDD.APP.Infrastructure_Layer.Repositories;

public static class EmployeeRepositoryExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        return services;
    }
}