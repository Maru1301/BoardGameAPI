namespace BoardGame.ApiCores
{
    public static class ApiCoresExtension
    {
        public static IServiceCollection AddApiCores(this IServiceCollection services)
        {
            services.AddScoped<MemberApiCore>();

            return services;
        }
    }
}
