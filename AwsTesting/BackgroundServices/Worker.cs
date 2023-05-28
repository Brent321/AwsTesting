using AwsTesting.IOptions;
using Microsoft.Extensions.Options;

namespace AwsTesting.BackgroundServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ConnectionStrings _config;

        public Worker(
            ILogger<Worker> logger,
            IOptions<ConnectionStrings> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Secret: {x}", _config.Mssql);
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
