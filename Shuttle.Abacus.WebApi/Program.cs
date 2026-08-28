using System.Data.Common;
using Asp.Versioning;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;
using Serilog;
using Shuttle.Abacus;
using Shuttle.Abacus.EventProcessing.v1.EventHandlers;
using Shuttle.Abacus.SqlServer;
using Shuttle.Abacus.WebApi;
using Shuttle.Abacus.WebApi.Endpoints;
using Shuttle.Access;
using Shuttle.Access.AspNetCore;
using Shuttle.Hopper;
using Shuttle.Hopper.AzureStorageQueues;
using Shuttle.Hopper.SqlServer.Subscription;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.EventProcessing;
using Shuttle.Recall.SqlServer.Storage;

DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);

var webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Host.UseSerilog();

var configuration = webApplicationBuilder.Configuration;
var services = webApplicationBuilder.Services;

configuration.AddUserSecrets<Program>().AddEnvironmentVariables();

var abacusConnectionString = configuration.GetConnectionString("Abacus") ?? "Missing connection string 'Abacus'.";

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var apiVersion1 = new ApiVersion(1, 0);

services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = apiVersion1;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddOpenApi(options =>
    {
        options.Document.AddSchemaTransformer((schema, _, _) =>
        {
            schema.Title = schema.Title?.Replace("+", "_");
            return Task.CompletedTask;
        });
    });

services
    .AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    })
    .AddEndpointsApiExplorer();

services
    .AddScoped<MessageDispatcher>()
    .AddAbacus()
    .UseSqlServer(options =>
    {
        options.ConnectionString = abacusConnectionString;
    })
    .Services
    .AddAccess(options =>
    {
        configuration.GetSection(AccessOptions.SectionName).Bind(options);
    })
    .Services
    .AddAccessAuthorization(options =>
    {
        configuration.GetSection(AccessAuthorizationOptions.SectionName).Bind(options);
    })
    .Services
    .AddHopper(options =>
    {
        configuration.GetSection(HopperOptions.SectionName).Bind(options);
    })
    .UseAzureStorageQueues(builder =>
    {
        builder.Configure("azure", options =>
        {
            configuration.GetSection($"{AzureStorageQueueOptions.SectionName}:Abacus").Bind(options);

            if (string.IsNullOrWhiteSpace(options.StorageAccount))
            {
                options.ConnectionString = configuration.GetConnectionString("azure") ?? string.Empty;
            }
        });
    })
    .AddMessageHandlersFrom(typeof(Program).Assembly)
    .UseSqlServerSubscription(sqlServerSubscriptionOptions =>
    {
        sqlServerSubscriptionOptions.ConnectionString = abacusConnectionString;
        sqlServerSubscriptionOptions.Schema = "abacus";
    });

var immediateConsistencyEnabled = configuration.GetValue<bool>($"{RecallOptions.SectionName}:EventProcessing:ImmediateConsistency:Enabled");

var recallBuilder = services
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
    .AddProjection<ArgumentHandler>(Shuttle.Abacus.ProjectionNames.Argument)
    .AddProjection<FormulaHandler>(Shuttle.Abacus.ProjectionNames.Formula)
    .AddProjection<MatrixHandler>(Shuttle.Abacus.ProjectionNames.Matrix)
    .AddProjection<TestHandler>(Shuttle.Abacus.ProjectionNames.Test);

if (immediateConsistencyEnabled)
{
    recallBuilder = recallBuilder.RegisterPrimitiveEventSequencing();
}

recallBuilder.Services
    .AddMediator()
    .AddParticipantsFrom(typeof(Shuttle.Abacus.Application.RegisterArgument).Assembly);

var app = webApplicationBuilder.Build();

var versionSet = app.NewApiVersionSet()
    .HasApiVersion(apiVersion1)
    .ReportApiVersions()
    .Build();

app.UseCors();

app.MapOpenApi().WithDocumentPerVersion();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Shuttle Abacus API")
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseAccessAuthorization();

app
    .MapArgumentEndpoints(versionSet)
    .MapFormulaEndpoints(versionSet)
    .MapMatrixEndpoints(versionSet)
    .MapTestEndpoints(versionSet)
    .MapServerEndpoints(versionSet);

await app.RunAsync();
