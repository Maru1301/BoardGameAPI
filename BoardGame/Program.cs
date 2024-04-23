using BoardGame.Repositories;
using BoardGame.Services;
using JWT;
using JWT.Extensions.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace BoardGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app, app.Environment);

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            RegisterScopedServices(services);

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MONGODB_URI");
                return new MongoClient(connectionString);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwt(options =>
            {
                options.Keys = new string[] { configuration.GetValue<string>("JwtSettings:Secret")?.ToString() ?? "defaultSecretKey" };
                options.VerifySignature = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAuthenticatedUser", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowEveryone", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        private static void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowEveryone");
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void RegisterScopedServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == "IService" || i.Name == "IRepository"));

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(i => i.Name != "IService" || i.Name != "IRepository");

                foreach (var interfaceType in interfaces)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }

    }
}