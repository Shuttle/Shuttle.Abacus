using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Shuttle.Abacus.Application;
using Shuttle.Access.AspNetCore;

namespace Shuttle.Abacus.WebApi.Endpoints;

public static class MatrixEndpoints
{
    private static Contracts.v1.Matrix Map(Query.Matrix matrix)
    {
        return new()
        {
            Id = matrix.Id,
            Name = matrix.Name,
            RowArgumentId = matrix.RowArgumentId,
            ColumnArgumentId = matrix.ColumnArgumentId,
            DataTypeName = matrix.DataTypeName
        };
    }

    private static Contracts.v1.MatrixConstraint Map(Query.Matrix.Constraint constraint)
    {
        return new()
        {
            Axis = constraint.Axis,
            Index = constraint.Index,
            Comparison = constraint.Comparison,
            Value = constraint.Value
        };
    }

    private static Contracts.v1.MatrixElement Map(Query.Matrix.Element element)
    {
        return new()
        {
            Row = element.Row,
            Column = element.Column,
            Value = element.Value
        };
    }

    public static WebApplication MapMatrixEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var apiVersion1 = new ApiVersion(1, 0);

        app.MapPost("/v{version:apiVersion}/matrices/search", PostSearch)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        app.MapPost("/v{version:apiVersion}/matrices", Post)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        app.MapGet("/v{version:apiVersion}/matrices/{id:Guid}", Get)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        app.MapGet("/v{version:apiVersion}/matrices/{id:Guid}/constraints", Constraints)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        app.MapPost("/v{version:apiVersion}/matrices/{id:Guid}/constraints", PostConstraint)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        app.MapGet("/v{version:apiVersion}/matrices/{id:Guid}/elements", Elements)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        app.MapPost("/v{version:apiVersion}/matrices/{id:Guid}/elements", PostElement)
            .WithTags("Matrices").WithApiVersionSet(versionSet).MapToApiVersion(apiVersion1)
            .RequirePermission(SystemPermissions.Manage.Matrices);

        return app;
    }

    private static async Task<IResult> PostSearch(IMatrixQuery matrixQuery, [FromBody] Contracts.v1.Matrix.Specification specification)
    {
        var search = new Query.Matrix.Specification();

        if (!string.IsNullOrWhiteSpace(specification.NameMatch))
        {
            search.WithNameMatch(specification.NameMatch);
        }

        search.AddIds(specification.Ids);

        return Results.Ok((await matrixQuery.SearchAsync(search)).Select(Map).ToList());
    }

    private static async Task<IResult> Post([FromBody] Contracts.v1.Matrix message, MessageDispatcher messageDispatcher)
    {
        // Matrix uniquely supports true re-registration by id (an "edit"), matching the domain's `Register`
        // method — a client that already has a matrix id sends it back to update the same matrix.
        var id = message.Id ?? Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterMatrix { Id = id, Name = message.Name, RowArgumentId = message.RowArgumentId, ColumnArgumentId = message.ColumnArgumentId, DataTypeName = message.DataTypeName },
            () => new RegisterMatrix(id, message.Name, message.RowArgumentId, message.ColumnArgumentId, message.DataTypeName));

        return Results.Accepted();
    }

    private static async Task<IResult> Get(Guid id, IMatrixQuery matrixQuery)
    {
        var matrix = (await matrixQuery.SearchAsync(new Query.Matrix.Specification().AddId(id))).SingleOrDefault();

        return matrix != null ? Results.Ok(Map(matrix)) : Results.NotFound();
    }

    private static async Task<IResult> Constraints(Guid id, IMatrixQuery matrixQuery)
    {
        return Results.Ok((await matrixQuery.ConstraintsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostConstraint(Guid id, [FromBody] Contracts.v1.MatrixConstraint message, MessageDispatcher messageDispatcher)
    {
        if (string.IsNullOrEmpty(message.Axis) ||
            !(message.Axis.Equals("Row", StringComparison.InvariantCultureIgnoreCase) || message.Axis.Equals("Column", StringComparison.InvariantCultureIgnoreCase)) ||
            message.Index < 1 ||
            string.IsNullOrEmpty(message.Comparison) ||
            string.IsNullOrEmpty(message.Value))
        {
            return Results.BadRequest();
        }

        var constraintId = Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterMatrixConstraint { Id = constraintId, MatrixId = id, Axis = message.Axis, Index = message.Index, Comparison = message.Comparison, Value = message.Value },
            () => new RegisterMatrixConstraint(constraintId, id, message.Axis, message.Index, message.Comparison, message.Value));

        return Results.Accepted();
    }

    private static async Task<IResult> Elements(Guid id, IMatrixQuery matrixQuery)
    {
        return Results.Ok((await matrixQuery.ElementsAsync(id)).Select(Map).ToList());
    }

    private static async Task<IResult> PostElement(Guid id, [FromBody] Contracts.v1.MatrixElement message, MessageDispatcher messageDispatcher)
    {
        if (message.Row < 1 || message.Column < 1 || string.IsNullOrEmpty(message.Value))
        {
            return Results.BadRequest();
        }

        var elementId = Guid.NewGuid();

        await messageDispatcher.DispatchAsync(
            () => new Messages.v1.RegisterMatrixElement { Id = elementId, MatrixId = id, Row = message.Row, Column = message.Column, Value = message.Value },
            () => new RegisterMatrixElement(elementId, id, message.Row, message.Column, message.Value));

        return Results.Accepted();
    }
}
