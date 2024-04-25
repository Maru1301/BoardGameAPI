using BoardGame.Filters;
using BoardGame.Infrastractures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

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
            services.AddSingleton<JWTHelper>();
            // Register using option mode
            services.Configure<JwtSettingsOptions>(configuration.GetSection("JwtSettings"));
            // Set authentication method
            services
              // Authenticate using bearer token and jwt format for token
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // Allow [Authorize] to determine roles
                      RoleClaimType = "roles",
                      // Validate the issuer by default
                      ValidateIssuer = true,
                      ValidIssuer = configuration.GetValue<string>("JwtSettings:ValidIssuer"),
                      // Do not validate the audience
                      ValidateAudience = false,
                      // Validate only if the token contains a key, usually only the signature
                      ValidateIssuerSigningKey = true,
                      // Key used for the signature
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Secret") ?? string.Empty))
                  };
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", policy =>
                    policy.RequireRole(Roles.Admin));
                options.AddPolicy("RequireMember", policy =>
                    policy.RequireRole(Roles.Member));
            });

            services.AddControllers();
            RegisterScopedServices(services);
            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MONGODB_URI");
                return new MongoClient(connectionString);
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