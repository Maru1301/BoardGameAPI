using NLog.Web;
using BoardGame.Hubs;

namespace BoardGame
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            builder.Logging.ClearProviders();

            if (!builder.Environment.IsDevelopment())
            {
                builder.Host.UseNLog();
            }

            var app = builder.Build();

            startup.Configure(app, app.Environment);

            app.MapControllers();
            app.MapHub<GameHub>("/gameHub");

            app.Run();
        }
    }
}