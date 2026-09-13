using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.Application;
using Shuttle.Access.AspNetCore;

namespace Shuttle.Abacus.WebApi.Endpoints;

public static class AlgorithmEndpoints
{
    private static Contracts.v1.Algorithm Map(Query.Algorithm algorithm)
    {
        return new()
        {
            Id = algorithm.Id,
            Name = algorithm.Name
        };
    }

    private static Contracts.v1.AlgorithmOperation Map(Query.Algorithm.Operation operation)
    {
        return new()
        {
            Id = operation.Id,
            Operation = operation.OperationName,
            ValueProviderName = operation.ValueProviderName,
            InputParameter = operation.InputParameter
        };
    }

    private static Contracts.v1.AlgorithmConstraint Map(Query.Algorithm.Constraint constraint)
    {
        return new()
        {
            Id = constraint.Id,
            ArgumentId = constraint.ArgumentId,
            Comparison = constraint.Comparison,
            Value = constraint.Value
        };
    }

    public static WebApplication MapAlgorithmEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var apiVersion1 = new ApiVersion(1, 0);

        app.MapPost("/v{version:apiVersion}/algorithms/search", PostSearch)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapPost("/v{version:apiVersion}/algorithms", Post)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapGet("/v{version:apiVersion}/algorithms/{id:Guid}", Get)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapPatch("/v{version:apiVersion}/algorithms/{id:Guid}/name", PatchName)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapDelete("/v{version:apiVersion}/algorithms/{id:Guid}", Delete)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapGet("/v{version:apiVersion}/algorithms/{id:Guid}/operations", Operations)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapPost("/v{version:apiVersion}/algorithms/{id:Guid}/operations", PostOperation)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapDelete("/v{version:apiVersion}/algorithms/{algorithmId:Guid}/operations/{operationId:Guid}", DeleteOperation)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapGet("/v{version:apiVersion}/algorithms/{id:Guid}/constraints", Constraints)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapPost("/v{version:apiVersion}/algorithms/{id:Guid}/constraints", PostConstraint)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        app.MapDelete("/v{version:apiVersion}/algorithms/{algorithmId:Guid}/constraints/{constraintId:Guid}", DeleteConstraint)
            .WithTags("Algorithms").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Algorithms);

        return app;
    }

    private static async Task<IResult> PostSearch(IAlgorithmQuery algorithmQuery, [FromBody] Contracts.v1.Algorithm.Specification specification)
    {
        var search = new Query.Algorithm.Specification();

        if (!string.IsNullOrWhiteSpace(specification.NameMatch))
        {
            search.WithNameMatch(specification.NameMatch);
        }

        search.AddIds(specification.Ids);

        return Results.Ok((await algorithmQuery.SearchAsync(search)).Select(Map).ToList());
    }

    private static async Task<IResult> Post([FromBody] Contracts.v1.Algorithm message, MessageDispatcher messageDispatcher)
    {
        var id = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterAlgorithm { Id = id, Name = message.Name },
            () => new RegisterAlgorithm(id, message.Name));

        return Results.Accepted();
    }

    private static async Task<IResult> Get(Guid id, IAlgorithmQuery algorithmQuery)
    {
        var algorithm = (await algorithmQuery.SearchAsync(new Query.Algorithm.Specification().AddId(id))).SingleOrDefault();

        return algorithm != null ? Results.Ok(Map(algorithm)) : Results.NotFound();
    }

    private static async Task<IResult> PatchName(Guid id, [FromBody] Contracts.v1.RenameAlgorithm message, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RenameAlgorithm { AlgorithmId = id, Name = message.Name },
            () => new RenameAlgorithm(id, message.Name));

        return Results.Accepted();
    }

    private static async Task<IResult> Delete(Guid id, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveAlgorithm { AlgorithmId = id },
            () => new RemoveAlgorithm(id));

        return Results.Accepted();
    }

    private static async Task<IResult> Operations(Guid id, IAlgorithmQuery algorithmQuery)
    {
        return Results.Ok((await algorithmQuery.OperationsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostOperation(Guid id, [FromBody] Contracts.v1.AlgorithmOperation message, MessageDispatcher messageDispatcher)
    {
        var operationId = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterAlgorithmOperation { Id = operationId, AlgorithmId = id, Operation = message.Operation, ValueProviderName = message.ValueProviderName, InputParameter = message.InputParameter },
            () => new RegisterAlgorithmOperation(operationId, id, message.Operation, message.ValueProviderName, message.InputParameter));

        return Results.Accepted();
    }

    private static async Task<IResult> DeleteOperation(Guid algorithmId, Guid operationId, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveAlgorithmOperation { AlgorithmId = algorithmId, OperationId = operationId },
            () => new RemoveAlgorithmOperation(algorithmId, operationId));

        return Results.Accepted();
    }

    private static async Task<IResult> Constraints(Guid id, IAlgorithmQuery algorithmQuery)
    {
        return Results.Ok((await algorithmQuery.ConstraintsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostConstraint(Guid id, [FromBody] Contracts.v1.AlgorithmConstraint message, MessageDispatcher messageDispatcher)
    {
        var constraintId = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterAlgorithmConstraint { Id = constraintId, AlgorithmId = id, ArgumentId = message.ArgumentId, Comparison = message.Comparison, Value = message.Value },
            () => new RegisterAlgorithmConstraint(constraintId, id, message.ArgumentId, message.Comparison, message.Value));

        return Results.Accepted();
    }

    private static async Task<IResult> DeleteConstraint(Guid algorithmId, Guid constraintId, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveAlgorithmConstraint { AlgorithmId = algorithmId, ConstraintId = constraintId },
            () => new RemoveAlgorithmConstraint(algorithmId, constraintId));

        return Results.Accepted();
    }
}
