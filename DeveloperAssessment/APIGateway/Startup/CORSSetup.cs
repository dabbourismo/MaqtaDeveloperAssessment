namespace APIGateway.Startup
{
    public static class CORSSetup
    {
        public static IServiceCollection RegisterCors(this IServiceCollection services,string enableCorsPolicy,string origins)
        {
            services.AddCors(x =>
            {
                x.AddPolicy(enableCorsPolicy, z =>
                {
                    z.WithOrigins(origins)
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials();
                });
            });
            return services;
        }
    }
}
