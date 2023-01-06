using BusinessLayer;
using DataAccess.EmployeeRepository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APIGateway.Startup
{
    public static class DependancyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }
    }
}
