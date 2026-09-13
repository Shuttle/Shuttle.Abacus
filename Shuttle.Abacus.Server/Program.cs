using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Shuttle.Abacus.EventProcessing.v1.EventHandlers;
using Shuttle.Abacus.SqlServer;
using Shuttle.Hopper;
using Shuttle.Hopper.AzureStorageQueues;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.EventProcessing;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);

        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddUserSecrets<Program>().AddEnvironmentVariables();

        var configuration = builder.Configuration;
        var services = builder.Services;

        var abacusConnectionString = configuration.GetConnectionString("Abacus") ?? "Missing connection string 'Abacus'.";

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Services.AddSerilog();

        services
            .AddAbacus()
            .UseSqlServer(options =>
            {
                options.ConnectionString = abacusConnectionString;
            });

        services
            .AddHopper(options =>
            {
                configuration.GetSection(HopperOptions.SectionName).Bind(options);
            })
            .UseAzureStorageQueues(azureBuilder =>
            {
                azureBuilder.Configure("azure", options =>
                {
                    configuration.GetSection($"{AzureStorageQueueOptions.SectionName}:Abacus").Bind(options);

                    if (string.IsNullOrWhiteSpace(options.StorageAccount))
                    {
                        options.ConnectionString = configuration.GetConnectionString("azure") ?? string.Empty;
                    }
                });
            })
            .AddMessageHandlersFrom(typeof(Program).Assembly);

        services
            .AddRecall(options =>
            {
                configuration.GetSection(RecallOptions.SectionName).Bind(options);
            })
            .UseSqlServerEventStorage(options =>
            {
                options.ConnectionString = abacusConnectionString;
                options.Schema = "abacus";
            })
            .UseSqlServerEventProcessing()
            .AddProjection<ArgumentHandler>(ProjectionNames.Argument)
            .AddProjection<AlgorithmHandler>(ProjectionNames.Algorithm)
            .AddProjection<MatrixHandler>(ProjectionNames.Matrix)
            .AddProjection<TestHandler>(ProjectionNames.Test);

        services
            .AddMediator()
            .AddParticipantsFrom(typeof(Application.RegisterArgument).Assembly);

        var host = builder.Build();

        await host.RunAsync();
    }
}
