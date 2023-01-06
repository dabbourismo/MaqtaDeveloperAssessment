namespace APIGateway.Startup
{
    public static class CORSSetup
    {
        public static IServiceCollection RegisterCors(this IServiceCollection services,string enableCorsPolicy)
        {
            services.AddCors(x =>
            {
                x.AddPolicy(enableCorsPolicy, z =>
                {
                    z.WithOrigins("http://localhost:4200")
                    //.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });
            return services;
        }
    }
}
