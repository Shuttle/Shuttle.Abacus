using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.Application;
using Shuttle.Access.AspNetCore;

namespace Shuttle.Abacus.WebApi.Endpoints;

public static class TestEndpoints
{
    private static Contracts.v1.Test Map(Query.Test test)
    {
        return new()
        {
            Id = test.Id,
            Name = test.Name,
            AlgorithmId = test.AlgorithmId,
            ExpectedResult = test.ExpectedResult,
            ExpectedResultDataTypeName = test.ExpectedResultDataTypeName,
            Comparison = test.Comparison
        };
    }

    private static Contracts.v1.TestArgument Map(Query.Test.Argument argument)
    {
        return new()
        {
            ArgumentId = argument.ArgumentId,
            Value = argument.Value
        };
    }

    private static string AllMessages(Exception exception)
    {
        var messages = new List<string>();
        var current = (Exception?)exception;

        while (current != null)
        {
            messages.Add(current.Message);
            current = current.InnerException;
        }

        return string.Join(" -> ", messages);
    }

    public static WebApplication MapTestEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var apiVersion1 = new ApiVersion(1, 0);

        app.MapPost("/v{version:apiVersion}/tests/search", PostSearch)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapPost("/v{version:apiVersion}/tests", Post)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapGet("/v{version:apiVersion}/tests/{id:Guid}", Get)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapDelete("/v{version:apiVersion}/tests/{id:Guid}", Delete)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapGet("/v{version:apiVersion}/tests/{id:Guid}/arguments", Arguments)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapPost("/v{version:apiVersion}/tests/{id:Guid}/arguments", PostArgument)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapDelete("/v{version:apiVersion}/tests/{testId:Guid}/arguments/{argumentId:Guid}", DeleteArgument)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        app.MapGet("/v{version:apiVersion}/tests/{id:Guid}/run", Run)
            .WithTags("Tests").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Tests);

        return app;
    }

    private static async Task<IResult> PostSearch(ITestQuery testQuery, [FromBody] Contracts.v1.Test.Specification specification)
    {
        var search = new Query.Test.Specification();

        if (!string.IsNullOrWhiteSpace(specification.NameMatch))
        {
            search.WithNameMatch(specification.NameMatch);
        }

        search.AddIds(specification.Ids);

        return Results.Ok((await testQuery.SearchAsync(search)).Select(Map).ToList());
    }

    private static async Task<IResult> Post([FromBody] Contracts.v1.Test message, MessageDispatcher messageDispatcher)
    {
        if (message.AlgorithmId.Equals(Guid.Empty) ||
            string.IsNullOrWhiteSpace(message.Name) ||
            string.IsNullOrWhiteSpace(message.Comparison) ||
            string.IsNullOrWhiteSpace(message.ExpectedResult) ||
            string.IsNullOrWhiteSpace(message.ExpectedResultDataTypeName))
        {
            return Results.BadRequest();
        }

        var id = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterTest { Id = id, Name = message.Name, AlgorithmId = message.AlgorithmId, ExpectedResult = message.ExpectedResult, ExpectedResultDataTypeName = message.ExpectedResultDataTypeName, Comparison = message.Comparison },
            () => new RegisterTest(id, message.Name, message.AlgorithmId, message.ExpectedResult, message.ExpectedResultDataTypeName, message.Comparison));

        return Results.Accepted();
    }

    private static async Task<IResult> Get(Guid id, ITestQuery testQuery)
    {
        var test = (await testQuery.SearchAsync(new Query.Test.Specification().AddId(id))).SingleOrDefault();

        return test != null ? Results.Ok(Map(test)) : Results.NotFound();
    }

    private static async Task<IResult> Delete(Guid id, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveTest { TestId = id },
            () => new RemoveTest(id));

        return Results.Accepted();
    }

    private static async Task<IResult> Arguments(Guid id, ITestQuery testQuery)
    {
        return Results.Ok((await testQuery.ArgumentsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostArgument(Guid id, [FromBody] Contracts.v1.TestArgument message, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterTestArgument { TestId = id, ArgumentId = message.ArgumentId, Value = message.Value },
            () => new RegisterTestArgument(id, message.ArgumentId, message.Value));

        return Results.Accepted();
    }

    private static async Task<IResult> DeleteArgument(Guid testId, Guid argumentId, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveTestArgument { TestId = testId, ArgumentId = argumentId },
            () => new RemoveTestArgument(testId, argumentId));

        return Results.Accepted();
    }

    private static async Task<IResult> Run(Guid id, ITestRepository testRepository, IExecutionService executionService, IValueComparer valueComparer, CancellationToken cancellationToken)
    {
        var test = await testRepository.GetAsync(id, cancellationToken);
        var executionContext = await executionService.ExecuteAsync(test.AlgorithmId, test.ArgumentValues(), new ContextLogger(ContextLogLevel.Verbose), cancellationToken);
        var result = executionContext.GetResult();

        return Results.Ok(new Contracts.v1.TestRunResult
        {
            Passed = !executionContext.HasException &&
                      valueComparer.IsSatisfiedBy(test.ExpectedResultDataTypeName, test.ExpectedResult, "==", result.ToString(System.Globalization.CultureInfo.InvariantCulture)),
            Exception = executionContext.HasException ? AllMessages(executionContext.Exception!) : null,
            Result = result,
            LogLines = executionContext.Logger.Lines.Select(line => new Contracts.v1.TestRunLogLine { Indent = line.Indent, Text = line.Text }).ToList(),
            Results = executionContext.GetResults().Select(r => new Contracts.v1.TestRunAlgorithmResult { AlgorithmName = r.AlgorithmName, Value = r.Value, Depth = r.Depth }).ToList()
        });
    }
}
