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

            //清除預設映射
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //註冊JwtHelper
            services.AddSingleton<JWTHelper>();
            //使用選項模式註冊
            services.Configure<JwtSettingsOptions>(configuration.GetSection("JwtSettings"));
            //設定認證方式
            services
              //使用bearer token方式認證並且token用jwt格式
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // 可以讓[Authorize]判斷角色
                      RoleClaimType = "roles",
                      // 預設會認證發行人
                      ValidateIssuer = true,
                      ValidIssuer = configuration.GetValue<string>("JwtSettings:ValidIssuer"),
                      // 不認證使用者
                      ValidateAudience = false,
                      // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                      ValidateIssuerSigningKey = true,
                      // 簽章所使用的key
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
                // 說明api如何受到保護
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    // 選擇類型，type選擇http時，透過swagger畫面做認證時可以省略Bearer前綴詞(如下圖)
                    Type = SecuritySchemeType.Http,
                    // 採用Bearer token
                    Scheme = "Bearer",
                    // bearer格式使用jwt
                    BearerFormat = "JWT",
                    // 認證放在http request的header上
                    In = ParameterLocation.Header,
                    // 描述
                    Description = "JWT驗證描述"
                });
                // 製作額外的過濾器，過濾Authorize、AllowAnonymous，甚至是沒有打attribute
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