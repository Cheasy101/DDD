using DDD.APP.ApplicationLayer.Services;

namespace DDD.APP.ApplicationLayer.Interfaces;

public  static class EmployeeServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      return  services.AddScoped<IEmployeeService, EmployeeService>();
    }
}