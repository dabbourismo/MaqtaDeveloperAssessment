using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIGateway.Startup
{
    public static class JwtSetup
    {
        public static IServiceCollection RegisterJwt(this IServiceCollection services,IConfigurationSection jwt)
        {
            
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = Convert.ToBoolean(jwt["ValidateIssuer"]),
                    ValidateAudience = Convert.ToBoolean(jwt["ValidateAudience"]),
                    ValidateLifetime = Convert.ToBoolean(jwt["ValidateLifetime"]),
                    ValidateIssuerSigningKey = Convert.ToBoolean(jwt["ValidateIssuerSigningKey"]),
                    ValidIssuer = jwt["ValidIssuer"],
                    ValidAudience = jwt["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["jwtKey"]))
                };
            });
            return services;
        }
    }
}
