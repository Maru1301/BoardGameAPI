using BoardGame.Models.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace BoardGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register each type of services as a scoped service
            var assembly = Assembly.GetExecutingAssembly();
            var ServiceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == "IService"));
            foreach (var serviceType in ServiceTypes)
            {
                var interfaceType = serviceType.GetInterfaces().First(i => i.Name != "IService");
                builder.Services.AddScoped(interfaceType, serviceType);
            }

            // Register each type of repositories as a scoped service
            var RepositoryTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == "IRepository"));
            foreach (var repositoryType in RepositoryTypes)
            {
                var interfaceType = repositoryType.GetInterfaces().First(i => i.Name != "IRepository");
                builder.Services.AddScoped(interfaceType, repositoryType);
            }

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}