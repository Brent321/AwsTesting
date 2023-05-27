using Amazon.CloudWatchLogs;
using AwsTesting;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
builder.Services.AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
