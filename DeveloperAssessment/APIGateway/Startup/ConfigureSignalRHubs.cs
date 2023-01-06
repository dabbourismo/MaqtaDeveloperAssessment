using APIGateway.SignalRHubs;

namespace APIGateway.Startup
{
    public static class ConfigureSignalRHubs
    {
        public static WebApplication ConfigureHubs(this WebApplication app)
        {
            app.MapHub<NotificationsHub>("/notifications");
            return app;
        }

        public static IServiceCollection RegisterSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }
    }
}
