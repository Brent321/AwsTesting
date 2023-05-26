using AwsTesting.BackgroundServices;

namespace AwsTesting
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }
    }
}
