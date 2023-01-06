using Microsoft.EntityFrameworkCore;
using Models;

namespace APIGateway.Startup
{
    public static class DbContextSetup
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
