using BusinessLayer;
using DataAccess.EmployeeRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Reflection;
using System.Text;
using APIGateway.Startup;
using APIGateway.SignalRHubs;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var enableCorsPolicy = builder.Configuration.GetSection("keys")["jwtKey"];
            builder.Services.RegisterCors(enableCorsPolicy);

            builder.Services.RegisterJwt(builder.Configuration.GetSection("keys"));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.RegisterServices();

            builder.Services.RegisterSignalR();

            builder.Services.RegisterSwagger();

            builder.Services.RegisterDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));



            var app = builder.Build();
            app.ConfigSwagger();
            app.UseHttpsRedirection();
            app.UseCors(enableCorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            //app.MapHub<NotificationsHub>("/notifications");
            app.ConfigureHubs();
            app.Run();
        }
    }
}