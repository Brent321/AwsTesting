using AwsTesting.IOptions;
using Microsoft.Extensions.Options;

namespace AwsTesting.BackgroundServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ConnectionStrings _connectionStrings;

        public Worker(
            ILogger<Worker> logger,
            IOptions<ConnectionStrings> connectionStrings)
        {
            _logger = logger;
            _connectionStrings = connectionStrings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Secret: {x}", _connectionStrings.Mssql);
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
