using BoardGame.Filters;
using BoardGame.Infrastractures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

            //�M���w�]�M�g
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //���UJwtHelper
            services.AddSingleton<JWTHelper>();
            //�ϥοﶵ�Ҧ����U
            services.Configure<JwtSettingsOptions>(configuration.GetSection("JwtSettings"));
            //�]�w�{�Ҥ覡
            services
              //�ϥ�bearer token�覡�{�ҨåBtoken��jwt�榡
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // �i�H��[Authorize]�P�_����
                      RoleClaimType = "roles",
                      // �w�]�|�{�ҵo��H
                      ValidateIssuer = true,
                      ValidIssuer = configuration.GetValue<string>("JwtSettings:ValidIssuer"),
                      // ���{�ҨϥΪ�
                      ValidateAudience = false,
                      // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
                      ValidateIssuerSigningKey = true,
                      // ñ���ҨϥΪ�key
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Secret")))
                  };
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
                // ����api�p�����O�@
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    // ��������Atype���http�ɡA�z�Lswagger�e�����{�Үɥi�H�ٲ�Bearer�e���(�p�U��)
                    Type = SecuritySchemeType.Http,
                    // �ĥ�Bearer token
                    Scheme = "Bearer",
                    // bearer�榡�ϥ�jwt
                    BearerFormat = "JWT",
                    // �{�ҩ�bhttp request��header�W
                    In = ParameterLocation.Header,
                    // �y�z
                    Description = "JWT���Ҵy�z"
                });
                // �s�@�B�~���L�o���A�L�oAuthorize�BAllowAnonymous�A�ƦܬO�S����attribute
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