using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.Application;
using Shuttle.Access.AspNetCore;

namespace Shuttle.Abacus.WebApi.Endpoints;

public static class FormulaEndpoints
{
    private static Contracts.v1.Formula Map(Query.Formula formula)
    {
        return new()
        {
            Id = formula.Id,
            Name = formula.Name
        };
    }

    private static Contracts.v1.FormulaOperation Map(Query.Formula.Operation operation)
    {
        return new()
        {
            Id = operation.Id,
            Operation = operation.OperationName,
            ValueProviderName = operation.ValueProviderName,
            InputParameter = operation.InputParameter
        };
    }

    private static Contracts.v1.FormulaConstraint Map(Query.Formula.Constraint constraint)
    {
        return new()
        {
            Id = constraint.Id,
            ArgumentId = constraint.ArgumentId,
            Comparison = constraint.Comparison,
            Value = constraint.Value
        };
    }

    public static WebApplication MapFormulaEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var apiVersion1 = new ApiVersion(1, 0);

        app.MapPost("/v{version:apiVersion}/formulas/search", PostSearch)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapPost("/v{version:apiVersion}/formulas", Post)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapGet("/v{version:apiVersion}/formulas/{id:Guid}", Get)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapPatch("/v{version:apiVersion}/formulas/{id:Guid}/name", PatchName)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapDelete("/v{version:apiVersion}/formulas/{id:Guid}", Delete)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapGet("/v{version:apiVersion}/formulas/{id:Guid}/operations", Operations)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapPost("/v{version:apiVersion}/formulas/{id:Guid}/operations", PostOperation)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapDelete("/v{version:apiVersion}/formulas/{formulaId:Guid}/operations/{operationId:Guid}", DeleteOperation)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapGet("/v{version:apiVersion}/formulas/{id:Guid}/constraints", Constraints)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapPost("/v{version:apiVersion}/formulas/{id:Guid}/constraints", PostConstraint)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        app.MapDelete("/v{version:apiVersion}/formulas/{formulaId:Guid}/constraints/{constraintId:Guid}", DeleteConstraint)
            .WithTags("Formulas").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Formulas);

        return app;
    }

    private static async Task<IResult> PostSearch(IFormulaQuery formulaQuery, [FromBody] Contracts.v1.Formula.Specification specification)
    {
        var search = new Query.Formula.Specification();

        if (!string.IsNullOrWhiteSpace(specification.NameMatch))
        {
            search.WithNameMatch(specification.NameMatch);
        }

        search.AddIds(specification.Ids);

        return Results.Ok((await formulaQuery.SearchAsync(search)).Select(Map).ToList());
    }

    private static async Task<IResult> Post([FromBody] Contracts.v1.Formula message, MessageDispatcher messageDispatcher)
    {
        var id = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterFormula { Id = id, Name = message.Name },
            () => new RegisterFormula(id, message.Name));

        return Results.Accepted();
    }

    private static async Task<IResult> Get(Guid id, IFormulaQuery formulaQuery)
    {
        var formula = (await formulaQuery.SearchAsync(new Query.Formula.Specification().AddId(id))).SingleOrDefault();

        return formula != null ? Results.Ok(Map(formula)) : Results.NotFound();
    }

    private static async Task<IResult> PatchName(Guid id, [FromBody] Contracts.v1.RenameFormula message, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RenameFormula { FormulaId = id, Name = message.Name },
            () => new RenameFormula(id, message.Name));

        return Results.Accepted();
    }

    private static async Task<IResult> Delete(Guid id, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveFormula { FormulaId = id },
            () => new RemoveFormula(id));

        return Results.Accepted();
    }

    private static async Task<IResult> Operations(Guid id, IFormulaQuery formulaQuery)
    {
        return Results.Ok((await formulaQuery.OperationsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostOperation(Guid id, [FromBody] Contracts.v1.FormulaOperation message, MessageDispatcher messageDispatcher)
    {
        var operationId = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterFormulaOperation { Id = operationId, FormulaId = id, Operation = message.Operation, ValueProviderName = message.ValueProviderName, InputParameter = message.InputParameter },
            () => new RegisterFormulaOperation(operationId, id, message.Operation, message.ValueProviderName, message.InputParameter));

        return Results.Accepted();
    }

    private static async Task<IResult> DeleteOperation(Guid formulaId, Guid operationId, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveFormulaOperation { FormulaId = formulaId, OperationId = operationId },
            () => new RemoveFormulaOperation(formulaId, operationId));

        return Results.Accepted();
    }

    private static async Task<IResult> Constraints(Guid id, IFormulaQuery formulaQuery)
    {
        return Results.Ok((await formulaQuery.ConstraintsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostConstraint(Guid id, [FromBody] Contracts.v1.FormulaConstraint message, MessageDispatcher messageDispatcher)
    {
        var constraintId = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterFormulaConstraint { Id = constraintId, FormulaId = id, ArgumentId = message.ArgumentId, Comparison = message.Comparison, Value = message.Value },
            () => new RegisterFormulaConstraint(constraintId, id, message.ArgumentId, message.Comparison, message.Value));

        return Results.Accepted();
    }

    private static async Task<IResult> DeleteConstraint(Guid formulaId, Guid constraintId, MessageDispatcher messageDispatcher)
    {
        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RemoveFormulaConstraint { FormulaId = formulaId, ConstraintId = constraintId },
            () => new RemoveFormulaConstraint(formulaId, constraintId));

        return Results.Accepted();
    }
}
