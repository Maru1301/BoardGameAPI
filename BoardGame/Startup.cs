using BoardGame.Filters;
using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services.Interfaces;
using BoardGame.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using BoardGame.Providers.Interfaces;
using BoardGame.Providers;
using Microsoft.Extensions.Caching.Memory;
using BoardGame.ApiCores;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BoardGame
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowEveryone", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddSystemConfig(out Config config);
            services.AddJwtAuthentication(config);
            services.AddControllers();
            services.RegisterScopedServices(config);
            services.AddApiCores();

            var mongoDBSettings = config.MongoDBConfig;
            services.AddDbContext<AppDbContext>(options =>
                options.UseMongoDB(mongoDBSettings?.AtlasURI ?? "", mongoDBSettings?.DatabaseName ?? ""), ServiceLifetime.Scoped);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // Describe how the api is protected
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    // When type is chosen as http, the Bearer prefix can be omitted when making authentication through the swagger interface (as shown in the figure below)
                    Type = SecuritySchemeType.Http,
                    // Use Bearer token
                    Scheme = "Bearer",
                    // Bearer format uses jwt
                    BearerFormat = "JWT",
                    // Authentication is placed in the header of the http request
                    In = ParameterLocation.Header,
                    // Description
                    Description = "JWT authentication description"
                });
                // Create additional filters to filter Authorize, AllowAnonymous, or even those without attributes
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowEveryone");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }

    public static class StartupExt
    {
        public static IServiceCollection AddSystemConfig(this IServiceCollection services, out Config outConfig)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "config.json");
            string json = File.ReadAllText(configFile);
            outConfig = JsonConvert.DeserializeObject<Config>(json);

            // Register the Configuration as a singleton
            services.AddSingleton<IConfig>(outConfig);

            return services;
        }

        public static IServiceCollection RegisterScopedServices(this IServiceCollection services, IConfig config)
        {
            //register UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //register cache provider
            services.AddCacheProvider(config);

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

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Config config)
        {
            var jwtSettings = config.JwtConfig;

            // Clear the default mapping
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // Register JwtHelper
            services.AddScoped<JWTHelper>();

            // Set authentication method
            services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Authenticate using bearer token and jwt format for token
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // Allow [Authorize] to determine roles
                      RoleClaimType = ClaimTypes.Role,

                      // Validate the issuer
                      ValidateIssuer = jwtSettings.ValidateIssuer,
                      ValidIssuer = jwtSettings.ValidIssuer,

                      // Validate the audience
                      ValidateAudience = jwtSettings.ValidateAudience,
                      ValidAudience = jwtSettings.ValidAudience,

                      //Validate only if the token contains a key, usually only the signature
                      ValidateIssuerSigningKey = true,

                      // Key used for the signature
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey ?? string.Empty))
                  };
              });

            return services;
        }

        public static IServiceCollection AddCacheProvider(this IServiceCollection services, IConfig config)
        {
            if (!string.IsNullOrEmpty(config.RedisConfig.ConnectionString))
            {
                services.AddSingleton<ICacheProvider, RedisProvider>();
            }
            else
            {
                services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
            }

            return services;
        }
    }
}
