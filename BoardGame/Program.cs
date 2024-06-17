using BoardGame.Filters;
using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using NLog.Web;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app, app.Environment);

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
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

            // Clear the default mapping
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // Register JwtHelper
            services.AddScoped<JWTHelper>();
            // Register using option mode
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.Configure<JWTSettingsOptions>(jwtSettings);
            // Set authentication method
            services
              // Authenticate using bearer token and jwt format for token
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // Allow [Authorize] to determine roles
                      RoleClaimType = ClaimTypes.Role,
                      // Validate the issuer
                      ValidateIssuer = true,
                      ValidIssuer = jwtSettings.GetValue<string>("ValidIssuer"),
                      // Validate the audience
                      ValidateAudience = true,
                      ValidAudience = jwtSettings.GetValue<string>("ValidAudience"),
                      //Validate only if the token contains a key, usually only the signature
                      ValidateIssuerSigningKey = true,
                      // Key used for the signature
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("IssuerSigningKey") ?? string.Empty))
                  };
              });

            services.AddControllers();
            RegisterScopedServices(services);

            var mongoDBSettings = configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseMongoDB(mongoDBSettings?.AtlasURI ?? "", mongoDBSettings?.DatabaseName ?? ""), ServiceLifetime.Scoped);

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

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
        }

        private static void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowEveryone");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void RegisterScopedServices(IServiceCollection services)
        {
            //register UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //register redis service
            services.AddScoped<ICacheService, CacheService>();

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