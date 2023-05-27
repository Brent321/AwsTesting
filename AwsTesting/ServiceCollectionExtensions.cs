using Amazon.CloudWatchLogs;
using Amazon.SecretsManager;
using AwsTesting.BackgroundServices;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;

namespace AwsTesting
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }

        public static void SetupSecretManager(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddSecretsManager(configurator: options =>
            {
                var config = new AmazonSecretsManagerConfig()
                {
                    ServiceURL = "http://localstack:4566",
                    UseHttp = true
                };
                options.CreateClient = () => new AmazonSecretsManagerClient(config);
            });
        }

        public static void SetupLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();
            
            var clientConfig = new AmazonCloudWatchLogsConfig()
            {
                ServiceURL = "http://localstack:4566",
                UseHttp = true
            };
            var client = new AmazonCloudWatchLogsClient(clientConfig);

            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.AmazonCloudWatch(
                    logGroup: "test",
                    logStreamPrefix: DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                    createLogGroup: true,
                    cloudWatchClient: client)
                .CreateLogger();
            Log.Logger = log;
        }
    }
}
